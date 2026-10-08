using Neo4j.Driver;
using YouLearnGeometry.Models;

namespace YouLearnGeometry.Services;

public interface INeo4jService : IAsyncDisposable
{
    Task<CytoscapeGraphData> GetGraphDataAsync(string? minGradeFilter = null);
    Task<GraphNode?> GetNodeDetailsAsync(string nodeId);
    Task<List<GraphNode>> GetAllNodesAsync();
    Task<List<GraphRelationship>> GetAllRelationshipsAsync();
    Task AddOrUpdateNodeAsync(GraphNode node);
    Task DeleteNodeAsync(string nodeId);
    Task AddRelationshipAsync(GraphRelationship rel);
    Task DeleteRelationshipAsync(string sourceId, string targetId, string type);
    Task InitializeSchemaAndSeedAsync();
}

public class Neo4jService : INeo4jService
{
    private readonly IDriver _driver;
    private readonly string? _database;

    public Neo4jService(IConfiguration configuration)
    {
        var uri = configuration["Neo4j:Uri"] ?? throw new InvalidOperationException("Neo4j Uri missing");
        var user = configuration["Neo4j:Username"] ?? throw new InvalidOperationException("Neo4j Username missing");
        var password = configuration["Neo4j:Password"] ?? throw new InvalidOperationException("Neo4j Password missing");
        _database = configuration["Neo4j:Database"];

        _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
    }

    private IAsyncSession CreateSession()
    {
        return !string.IsNullOrEmpty(_database)
            ? _driver.AsyncSession(o => o.WithDatabase(_database))
            : _driver.AsyncSession();
    }

    public async Task<CytoscapeGraphData> GetGraphDataAsync(string? minGradeFilter = null)
    {
        var result = new CytoscapeGraphData();
        await using var session = CreateSession();

        string query = @"
            MATCH (n)
            OPTIONAL MATCH (n)-[r]->(m)
            RETURN n, r, m";

        var cursor = await session.RunAsync(query);
        var seenNodes = new HashSet<string>();
        var seenEdges = new HashSet<string>();

        while (await cursor.FetchAsync())
        {
            var nodeRecord = cursor.Current["n"]?.As<INode>();
            if (nodeRecord != null)
            {
                string id = nodeRecord.Properties.ContainsKey("id") ? nodeRecord.Properties["id"].As<string>() : nodeRecord.ElementId;
                if (!seenNodes.Contains(id))
                {
                    seenNodes.Add(id);
                    string label = nodeRecord.Properties.ContainsKey("label") ? nodeRecord.Properties["label"].As<string>() : id;
                    string type = nodeRecord.Properties.ContainsKey("type") ? nodeRecord.Properties["type"].As<string>() : "Shape";
                    string minGrade = nodeRecord.Properties.ContainsKey("minGrade") ? nodeRecord.Properties["minGrade"].As<string>() : "6";
                    string definition = nodeRecord.Properties.ContainsKey("definition") ? nodeRecord.Properties["definition"].As<string>() : "";
                    string sign = nodeRecord.Properties.ContainsKey("identificationSign") ? nodeRecord.Properties["identificationSign"].As<string>() : "";
                    string tip = nodeRecord.Properties.ContainsKey("learningTip") ? nodeRecord.Properties["learningTip"].As<string>() : "";

                    string color = id switch
                    {
                        "HinhHocPhang" => "#4338ca", // Deep Indigo root
                        var x when x.Contains("TamGiac") => "#059669", // Emerald Green for Triangles
                        var x when x.Contains("Tron") || x.Contains("Cung") || x.Contains("TiepTuyen") || x.Contains("GocNoiTiep") => "#e11d48", // Rose Pink for Circles
                        var x when x.Contains("DaGiac") || x.Contains("LucGiac") => "#7c3aed", // Violet for Regular Polygons
                        _ => "#2563eb" // Royal Blue for Quads
                    };

                    result.Nodes.Add(new CytoscapeNodeElement
                    {
                        Data = new CytoscapeNodeData
                        {
                            Id = id,
                            Label = label,
                            Type = type,
                            MinGrade = minGrade,
                            Definition = definition,
                            IdentificationSign = sign,
                            LearningTip = tip,
                            Color = color
                        }
                    });
                }
            }

            var relRecord = cursor.Current["r"]?.As<IRelationship>();
            var targetRecord = cursor.Current["m"]?.As<INode>();
            if (relRecord != null && nodeRecord != null && targetRecord != null)
            {
                string srcId = nodeRecord.Properties.ContainsKey("id") ? nodeRecord.Properties["id"].As<string>() : nodeRecord.ElementId;
                string tgtId = targetRecord.Properties.ContainsKey("id") ? targetRecord.Properties["id"].As<string>() : targetRecord.ElementId;
                string relType = relRecord.Type;
                string edgeId = $"{srcId}_{relType}_{tgtId}";

                if (!seenEdges.Contains(edgeId))
                {
                    seenEdges.Add(edgeId);
                    string label = relRecord.Properties.ContainsKey("label") ? relRecord.Properties["label"].As<string>() : relType;
                    string explanation = relRecord.Properties.ContainsKey("explanation") ? relRecord.Properties["explanation"].As<string>() : "";

                    result.Edges.Add(new CytoscapeEdgeElement
                    {
                        Data = new CytoscapeEdgeData
                        {
                            Id = edgeId,
                            Source = srcId,
                            Target = tgtId,
                            Type = relType,
                            Label = label,
                            Explanation = explanation
                        }
                    });
                }
            }
        }

        return result;
    }

    public async Task<GraphNode?> GetNodeDetailsAsync(string nodeId)
    {
        await using var session = CreateSession();
        string query = @"
            MATCH (n {id: $id})
            RETURN n";
        var cursor = await session.RunAsync(query, new { id = nodeId });
        if (await cursor.FetchAsync())
        {
            var nodeRecord = cursor.Current["n"].As<INode>();
            return MapNode(nodeRecord);
        }
        return null;
    }

    public async Task<List<GraphNode>> GetAllNodesAsync()
    {
        var list = new List<GraphNode>();
        await using var session = CreateSession();
        string query = "MATCH (n) RETURN n ORDER BY n.label";
        var cursor = await session.RunAsync(query);
        while (await cursor.FetchAsync())
        {
            list.Add(MapNode(cursor.Current["n"].As<INode>()));
        }
        return list;
    }

    public async Task<List<GraphRelationship>> GetAllRelationshipsAsync()
    {
        var list = new List<GraphRelationship>();
        await using var session = CreateSession();
        string query = @"
            MATCH (s)-[r]->(t)
            RETURN s.id AS sourceId, t.id AS targetId, type(r) AS relType, r.label AS label, r.explanation AS explanation
            ORDER BY sourceId";
        var cursor = await session.RunAsync(query);
        while (await cursor.FetchAsync())
        {
            list.Add(new GraphRelationship
            {
                SourceId = cursor.Current["sourceId"].As<string>(),
                TargetId = cursor.Current["targetId"].As<string>(),
                Type = cursor.Current["relType"].As<string>(),
                Label = cursor.Current["label"]?.As<string>() ?? "",
                Explanation = cursor.Current["explanation"]?.As<string>() ?? ""
            });
        }
        return list;
    }

    public async Task AddOrUpdateNodeAsync(GraphNode node)
    {
        await using var session = CreateSession();
        string query = @"
            MERGE (n {id: $id})
            SET n.label = $label,
                n.type = $type,
                n.definition = $definition,
                n.identificationSign = $identificationSign,
                n.learningTip = $learningTip,
                n.minGrade = $minGrade";
        await session.RunAsync(query, new
        {
            id = node.Id,
            label = node.Label,
            type = node.Type,
            definition = node.Definition,
            identificationSign = node.IdentificationSign,
            learningTip = node.LearningTip,
            minGrade = node.MinGrade
        });
    }

    public async Task DeleteNodeAsync(string nodeId)
    {
        await using var session = CreateSession();
        string query = "MATCH (n {id: $id}) DETACH DELETE n";
        await session.RunAsync(query, new { id = nodeId });
    }

    public async Task AddRelationshipAsync(GraphRelationship rel)
    {
        await using var session = CreateSession();
        string query = $@"
            MATCH (s {{id: $sourceId}}), (t {{id: $targetId}})
            MERGE (s)-[r:{rel.Type}]->(t)
            SET r.label = $label, r.explanation = $explanation";
        await session.RunAsync(query, new
        {
            sourceId = rel.SourceId,
            targetId = rel.TargetId,
            label = rel.Label,
            explanation = rel.Explanation
        });
    }

    public async Task DeleteRelationshipAsync(string sourceId, string targetId, string type)
    {
        await using var session = CreateSession();
        string query = $@"
            MATCH (s {{id: $sourceId}})-[r:{type}]->(t {{id: $targetId}})
            DELETE r";
        await session.RunAsync(query, new { sourceId, targetId });
    }

    public async Task InitializeSchemaAndSeedAsync()
    {
        await using var session = CreateSession();

        // Check if full graph (>= 20 nodes) is already present
        var checkCursor = await session.RunAsync("MATCH (n) RETURN count(n) AS cnt");
        await checkCursor.FetchAsync();
        long count = checkCursor.Current["cnt"].As<long>();
        if (count >= 20) return; // Already seeded with full curriculum

        // Clean out previous nodes to ensure fresh, consistent schema
        await session.RunAsync("MATCH (n) DETACH DELETE n");

        // Seed comprehensive Knowledge Graph for Geometry (SGK Toan 6, 7, 8, 9 - Bo Ket Noi Tri Thuc)
        string seedQuery = @"
            // 1. Root Concept
            CREATE (root:Concept {
                id: 'HinhHocPhang',
                label: 'Hình học phẳng THCS',
                type: 'Concept',
                minGrade: '6',
                definition: 'Phân môn toán học nghiên cứu các hình và tính chất của các hình cùng nằm trên một mặt phẳng hai chiều.',
                identificationSign: 'Các đối tượng cơ bản gồm: Điểm, Đoạn thẳng, Góc, Tam giác, Tứ giác, Đa giác đều và Đường tròn.',
                learningTip: 'Toàn bộ hình học phẳng THCS phát triển từ trực quan (Lớp 6) -> suy luận & chứng minh (Lớp 7, 8) -> hệ thức lượng & đường tròn (Lớp 9).'
            })

            // 2. Nhóm Tam giác
            CREATE (tg:Shape {
                id: 'HinhTamGiac',
                label: 'Hình tam giác',
                type: 'Shape',
                minGrade: '6',
                definition: 'Hình gồm ba đoạn thẳng nối ba điểm không thẳng hàng. Có 3 đỉnh, 3 cạnh và 3 góc trong.',
                identificationSign: 'Tổng ba góc trong một tam giác luôn bằng 180 độ. Bất đẳng thức tam giác: |b - c| < a < b + c.',
                learningTip: 'Tam giác là khối cơ bản cấu tạo nên mọi đa giác phẳng. Các đường đồng quy: Trọng tâm, Trực tâm, Tâm nội tiếp, Tâm ngoại tiếp.'
            })
            CREATE (tgCan:Shape {
                id: 'TamGiacCan',
                label: 'Tam giác cân',
                type: 'Shape',
                minGrade: '7',
                definition: 'Tam giác có hai cạnh bằng nhau.',
                identificationSign: 'Tam giác có hai cạnh bằng nhau hoặc có hai góc ở đáy bằng nhau.',
                learningTip: 'Trong tam giác cân, đường phân giác xuất phát từ đỉnh đồng thời là đường trung tuyến, đường cao và đường trung trực của cạnh đáy.'
            })
            CREATE (tgDeu:Shape {
                id: 'TamGiacDeu',
                label: 'Tam giác đều',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tam giác có ba cạnh bằng nhau.',
                identificationSign: 'Tam giác có ba cạnh bằng nhau hoặc tam giác có ba góc bằng nhau (đều bằng 60 độ); hoặc tam giác cân có một góc bằng 60 độ.',
                learningTip: 'Tam giác đều là đa giác đều 3 cạnh. Trọng tâm, trực tâm, tâm đường tròn nội tiếp và ngoại tiếp trùng nhau.'
            })
            CREATE (tgVuong:Shape {
                id: 'TamGiacVuong',
                label: 'Tam giác vuông',
                type: 'Shape',
                minGrade: '7',
                definition: 'Tam giác có một góc vuông (bằng 90 độ). Cạnh đối diện góc vuông là cạnh huyền, hai cạnh kề là cạnh góc vuông.',
                identificationSign: 'Tam giác có một góc vuông; hoặc tam giác thỏa mãn định lí Pythagore đảo: a² = b² + c².',
                learningTip: 'Định lí Pythagore: a² = b² + c². Trong tam giác vuông, đường trung tuyến ứng với cạnh huyền bằng nửa cạnh huyền.'
            })
            CREATE (tgVuongCan:Shape {
                id: 'TamGiacVuongCan',
                label: 'Tam giác vuông cân',
                type: 'Shape',
                minGrade: '7',
                definition: 'Tam giác vừa vuông vừa cân (có một góc vuông và hai cạnh góc vuông bằng nhau).',
                identificationSign: 'Tam giác vuông có hai cạnh góc vuông bằng nhau; hoặc tam giác cân có góc ở đỉnh bằng 90 độ.',
                learningTip: 'Hai góc nhọn ở đáy bằng nhau và đều bằng 45 độ. Cạnh huyền bằng cạnh góc vuông nhân căn bậc hai của 2.'
            })

            // 3. Nhóm Tứ giác
            CREATE (tuGiac:Shape {
                id: 'HinhTuGiac',
                label: 'Tứ giác lồi',
                type: 'Shape',
                minGrade: '8',
                definition: 'Đa giác có 4 cạnh mà bất kỳ đường thẳng nào chứa một cạnh cũng không chia tứ giác thành hai phần nằm ở hai nửa mặt phẳng khác nhau.',
                identificationSign: 'Định lý: Tổng bốn góc của một tứ giác luôn bằng 360 độ.',
                learningTip: 'Mọi tứ giác đều có thể chia thành hai hình tam giác bằng một đường chéo. Có 2 đường chéo.'
            })
            CREATE (hth:Shape {
                id: 'HinhThang',
                label: 'Hình thang',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có hai cạnh đối song song. Hai cạnh song song gọi là hai đáy, hai cạnh còn lại gọi là hai cạnh bên.',
                identificationSign: 'Tứ giác có ít nhất một cặp cạnh đối song song.',
                learningTip: 'Công thức diện tích: S = (a + b) * h / 2 (Đáy lớn cộng đáy bé nhân chiều cao chia hai).'
            })
            CREATE (htc:Shape {
                id: 'HinhThangCan',
                label: 'Hình thang cân',
                type: 'Shape',
                minGrade: '6',
                definition: 'Hình thang có hai góc kề một đáy bằng nhau.',
                identificationSign: 'Hình thang có 2 góc kề một đáy bằng nhau; hoặc hình thang có 2 đường chéo bằng nhau.',
                learningTip: 'Hình thang cân có hai cạnh bên bằng nhau, hai đường chéo bằng nhau và có 1 trục đối xứng. Luôn nội tiếp được trong đường tròn.'
            })
            CREATE (htv:Shape {
                id: 'HinhThangVuong',
                label: 'Hình thang vuông',
                type: 'Shape',
                minGrade: '8',
                definition: 'Hình thang có một cạnh bên vuông góc với hai đáy.',
                identificationSign: 'Hình thang có một góc vuông.',
                learningTip: 'Cạnh bên vuông góc với hai đáy chính là chiều cao của hình thang vuông.'
            })
            CREATE (hbh:Shape {
                id: 'HinhBinhHanh',
                label: 'Hình bình hành',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có các cặp cạnh đối song song.',
                identificationSign: '1. Các cạnh đối song song; 2. Các cạnh đối bằng nhau; 3. Hai cạnh đối song song và bằng nhau; 4. Các góc đối bằng nhau; 5. Hai đường chéo cắt nhau tại trung điểm mỗi đường.',
                learningTip: 'Hình bình hành có tâm đối xứng là giao điểm hai đường chéo. Diện tích S = a * h.'
            })
            CREATE (hcn:Shape {
                id: 'HinhChuNhat',
                label: 'Hình chữ nhật',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có bốn góc vuông.',
                identificationSign: '1. Tứ giác có 3 góc vuông; 2. Hình thang cân có 1 góc vuông; 3. Hình bình hành có 1 góc vuông; 4. Hình bình hành có 2 đường chéo bằng nhau.',
                learningTip: 'Hình chữ nhật có 2 trục đối xứng và 1 tâm đối xứng. Hai đường chéo bằng nhau và cắt nhau tại trung điểm. Luôn nội tiếp được trong đường tròn.'
            })
            CREATE (ht:Shape {
                id: 'HinhThoi',
                label: 'Hình thoi',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có bốn cạnh bằng nhau.',
                identificationSign: '1. Tứ giác có 4 cạnh bằng nhau; 2. Hình bình hành có 2 cạnh kề bằng nhau; 3. Hình bình hành có 2 đường chéo vuông góc; 4. Hình bình hành có 1 đường chéo là phân giác.',
                learningTip: 'Hình thoi có hai đường chéo vuông góc tại trung điểm mỗi đường và là phân giác các góc. Diện tích S = 1/2 * d1 * d2.'
            })
            CREATE (hv:Shape {
                id: 'HinhVuong',
                label: 'Hình vuông',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có bốn góc vuông và bốn cạnh bằng nhau.',
                identificationSign: '1. Hình chữ nhật có 2 cạnh kề bằng nhau; 2. Hình chữ nhật có 2 đường chéo vuông góc; 3. Hình thoi có 1 góc vuông; 4. Hình thoi có 2 đường chéo bằng nhau.',
                learningTip: 'Hình vuông là hình có độ đối xứng cao nhất: 4 trục đối xứng, 1 tâm đối xứng, vừa là hình chữ nhật vừa là hình thoi, vừa là đa giác đều 4 cạnh.'
            })

            // 4. Nhóm Đường tròn & Tứ giác nội tiếp
            CREATE (htron:Shape {
                id: 'HinhTron',
                label: 'Đường tròn',
                type: 'Shape',
                minGrade: '9',
                definition: 'Đường tròn tâm O bán kính R là hình gồm các điểm cách O một khoảng bằng R, kí hiệu (O; R).',
                identificationSign: 'Tập hợp các điểm cách đều tâm O một khoảng bằng bán kính R không đổi.',
                learningTip: 'Đường kính là dây cung lớn nhất (d = 2R). Đường kính vuông góc với một dây thì đi qua trung điểm dây đó. Có vô số trục đối xứng.'
            })
            CREATE (cungDay:Concept {
                id: 'CungVaDay',
                label: 'Cung và Dây cung',
                type: 'Concept',
                minGrade: '9',
                definition: 'Đoạn thẳng nối hai điểm trên đường tròn gọi là dây cung. Phần đường tròn giới hạn bởi hai điểm gọi là cung tròn.',
                identificationSign: 'Trong một đường tròn: Hai dây bằng nhau căng hai cung bằng nhau; Dây lớn hơn căng cung lớn hơn.',
                learningTip: 'Độ dài cung n độ: l = π * R * n / 180. Diện tích hình quạt tròn: S = π * R² * n / 360.'
            })
            CREATE (tt:Concept {
                id: 'TiepTuyen',
                label: 'Tiếp tuyến đường tròn',
                type: 'Concept',
                minGrade: '9',
                definition: 'Đường thẳng chỉ có một điểm chung với đường tròn gọi là tiếp tuyến của đường tròn đó.',
                identificationSign: 'Đường thẳng vuông góc với bán kính tại tiếp điểm là tiếp tuyến của đường tròn.',
                learningTip: 'Hai tiếp tuyến cắt nhau: Giao điểm cách đều hai tiếp điểm; tia nối từ giao điểm tới tâm là phân giác góc tạo bởi hai tiếp tuyến.'
            })
            CREATE (gnt:Concept {
                id: 'GocNoiTiep',
                label: 'Góc nội tiếp',
                type: 'Concept',
                minGrade: '9',
                definition: 'Góc có đỉnh nằm trên đường tròn và hai cạnh chứa hai dây cung của đường tròn đó.',
                identificationSign: 'Định lý: Số đo của góc nội tiếp bằng nửa số đo của cung bị chắn.',
                learningTip: 'Các góc nội tiếp cùng chắn một cung thì bằng nhau. Góc nội tiếp chắn nửa đường tròn là góc vuông (90 độ).'
            })
            CREATE (tgnt:Shape {
                id: 'TuGiacNoiTiep',
                label: 'Tứ giác nội tiếp',
                type: 'Shape',
                minGrade: '9',
                definition: 'Tứ giác có cả bốn đỉnh cùng nằm trên một đường tròn gọi là tứ giác nội tiếp đường tròn.',
                identificationSign: '1. Tứ giác có tổng hai góc đối diện bằng 180 độ; 2. Góc ngoài tại một đỉnh bằng góc trong đỉnh đối diện; 3. Hai đỉnh kề nhau cùng nhìn cạnh còn lại dưới một góc bằng nhau.',
                learningTip: 'Hình chữ nhật, hình vuông, hình thang cân luôn luôn là các tứ giác nội tiếp đường tròn!'
            })

            // 5. Nhóm Đa giác đều
            CREATE (dgd:Shape {
                id: 'DaGiacDeu',
                label: 'Đa giác đều',
                type: 'Shape',
                minGrade: '9',
                definition: 'Đa giác có tất cả các cạnh bằng nhau và tất cả các góc bằng nhau.',
                identificationSign: 'Đa giác lồi có tất cả các cạnh bằng nhau và các góc bằng nhau.',
                learningTip: 'Mọi đa giác đều luôn có một đường tròn ngoại tiếp và một đường tròn nội tiếp cùng tâm.'
            })
            CREATE (hlgd:Shape {
                id: 'HinhLucGiacDeu',
                label: 'Hình lục giác đều',
                type: 'Shape',
                minGrade: '6',
                definition: 'Đa giác đều có 6 cạnh bằng nhau và 6 góc bằng nhau (mỗi góc bằng 120 độ).',
                identificationSign: 'Ghép từ 6 tam giác đều chung đỉnh tâm đối xứng.',
                learningTip: 'Có 3 đường chéo chính bằng nhau và cắt nhau tại tâm đối xứng. Có 6 trục đối xứng và 1 tâm đối xứng.'
            })

            // ==========================================
            // RELATIONSHIPS
            // ==========================================
            // Gốc kế thừa
            CREATE (tg)-[:IS_A {label: 'thuộc lớp', explanation: 'Tam giác là hình đa giác phẳng 3 cạnh'}]->(root)
            CREATE (tuGiac)-[:IS_A {label: 'thuộc lớp', explanation: 'Tứ giác là hình đa giác phẳng 4 cạnh'}]->(root)
            CREATE (htron)-[:IS_A {label: 'thuộc lớp', explanation: 'Đường tròn là đường cong phẳng khép kín đặc biệt'}]->(root)
            CREATE (dgd)-[:IS_A {label: 'thuộc lớp', explanation: 'Đa giác đều là hình phẳng có các cạnh và góc bằng nhau'}]->(root)

            // Phân nhánh Tam giác
            CREATE (tgCan)-[:IS_A {label: 'là trường hợp của', explanation: 'Tam giác cân là tam giác có 2 cạnh bằng nhau'}]->(tg)
            CREATE (tgVuong)-[:IS_A {label: 'là trường hợp của', explanation: 'Tam giác vuông là tam giác có 1 góc bằng 90 độ'}]->(tg)
            CREATE (tgDeu)-[:IS_A {label: 'là trường hợp của', explanation: 'Tam giác đều là tam giác cân có cả 3 cạnh bằng nhau'}]->(tgCan)
            CREATE (tgVuongCan)-[:IS_A {label: 'là trường hợp của', explanation: 'Tam giác vuông cân vừa là tam giác vuông vừa là tam giác cân'}]->(tgCan)
            CREATE (tgVuongCan)-[:IS_A {label: 'là trường hợp của', explanation: 'Tam giác vuông cân là tam giác vuông có 2 cạnh góc vuông bằng nhau'}]->(tgVuong)
            CREATE (tgDeu)-[:IS_A {label: 'là trường hợp của', explanation: 'Tam giác đều là đa giác đều có 3 cạnh'}]->(dgd)

            // Dấu hiệu chuyển hóa Tam giác (TRANSFORMS_TO)
            CREATE (tgCan)-[:TRANSFORMS_TO {label: 'chuyển thành', explanation: 'Tam giác cân có 1 góc bằng 60 độ sẽ trở thành Tam giác đều'}]->(tgDeu)
            CREATE (tgVuong)-[:TRANSFORMS_TO {label: 'chuyển thành', explanation: 'Tam giác vuông có 2 cạnh góc vuông bằng nhau sẽ thành Tam giác vuông cân'}]->(tgVuongCan)

            // Phân nhánh Tứ giác
            CREATE (hth)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình thang là tứ giác có 2 cạnh đối song song'}]->(tuGiac)
            CREATE (htc)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình thang cân là hình thang có 2 góc kề một đáy bằng nhau'}]->(hth)
            CREATE (htv)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình thang vuông là hình thang có 1 góc vuông'}]->(hth)
            CREATE (hbh)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình bình hành là hình thang có 2 cạnh bên song song'}]->(hth)
            CREATE (hcn)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình chữ nhật là hình bình hành có 1 góc vuông'}]->(hbh)
            CREATE (ht)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình thoi là hình bình hành có 2 cạnh kề bằng nhau'}]->(hbh)
            CREATE (hv)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình vuông là hình chữ nhật có 4 cạnh bằng nhau'}]->(hcn)
            CREATE (hv)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình vuông là hình thoi có 4 góc vuông'}]->(ht)
            CREATE (hv)-[:IS_A {label: 'là trường hợp của', explanation: 'Hình vuông là đa giác đều có 4 cạnh'}]->(dgd)
            CREATE (hlgd)-[:IS_A {label: 'là trường hợp của', explanation: 'Lục giác đều là đa giác đều có 6 cạnh'}]->(dgd)

            // Dấu hiệu chuyển hóa Tứ giác (TRANSFORMS_TO)
            CREATE (hth)-[:TRANSFORMS_TO {label: 'chuyển thành', explanation: 'Hình thang có 2 góc kề một đáy bằng nhau hoặc 2 đường chéo bằng nhau thành Hình thang cân'}]->(htc)
            CREATE (hth)-[:TRANSFORMS_TO {label: 'chuyển thành', explanation: 'Hình thang có 2 cạnh bên song song hoặc 2 đáy bằng nhau thành Hình bình hành'}]->(hbh)
            CREATE (hbh)-[:TRANSFORMS_TO {label: 'chuyển thành', explanation: 'Hình bình hành có 1 góc vuông hoặc 2 đường chéo bằng nhau thành Hình chữ nhật'}]->(hcn)
            CREATE (hbh)-[:TRANSFORMS_TO {label: 'chuyển thành', explanation: 'Hình bình hành có 2 cạnh kề bằng nhau hoặc 2 đường chéo vuông góc thành Hình thoi'}]->(ht)
            CREATE (hcn)-[:TRANSFORMS_TO {label: 'chuyển thành', explanation: 'Hình chữ nhật có 2 cạnh kề bằng nhau hoặc 2 đường chéo vuông góc thành Hình vuông'}]->(hv)
            CREATE (ht)-[:TRANSFORMS_TO {label: 'chuyển thành', explanation: 'Hình thoi có 1 góc vuông hoặc 2 đường chéo bằng nhau thành Hình vuông'}]->(hv)

            // Phân nhánh Đường tròn & Tứ giác nội tiếp
            CREATE (cungDay)-[:RELATED_TO {label: 'thuộc về', explanation: 'Cung và dây là các thành phần cơ bản của đường tròn'}]->(htron)
            CREATE (tt)-[:RELATED_TO {label: 'liên hệ với', explanation: 'Tiếp tuyến vuông góc với bán kính tại tiếp điểm'}]->(htron)
            CREATE (gnt)-[:RELATED_TO {label: 'chắn cung của', explanation: 'Góc nội tiếp có số đo bằng một nửa số đo cung bị chắn'}]->(htron)
            CREATE (tgnt)-[:IS_A {label: 'thuộc lớp', explanation: 'Tứ giác nội tiếp là tứ giác có 4 đỉnh cùng thuộc một đường tròn'}]->(tuGiac)
            CREATE (tgnt)-[:RELATED_TO {label: 'nội tiếp trong', explanation: 'Bốn đỉnh của tứ giác cùng nằm trên một đường tròn'}]->(htron)

            // Các hình đặc biệt luôn nội tiếp đường tròn
            CREATE (hcn)-[:IS_A {label: 'luôn là', explanation: 'Hình chữ nhật có tổng hai góc đối bằng 90 + 90 = 180 độ nên luôn là tứ giác nội tiếp'}]->(tgnt)
            CREATE (hv)-[:IS_A {label: 'luôn là', explanation: 'Hình vuông luôn có tổng hai góc đối bằng 180 độ nên luôn là tứ giác nội tiếp'}]->(tgnt)
            CREATE (htc)-[:IS_A {label: 'luôn là', explanation: 'Hình thang cân luôn có tổng hai góc đối bằng 180 độ nên luôn là tứ giác nội tiếp'}]->(tgnt)
        ";

        await session.RunAsync(seedQuery);
    }

    private static GraphNode MapNode(INode record)
    {
        var node = new GraphNode
        {
            Id = record.Properties.ContainsKey("id") ? record.Properties["id"].As<string>() : record.ElementId,
            Label = record.Properties.ContainsKey("label") ? record.Properties["label"].As<string>() : "",
            Type = record.Properties.ContainsKey("type") ? record.Properties["type"].As<string>() : "Shape",
            MinGrade = record.Properties.ContainsKey("minGrade") ? record.Properties["minGrade"].As<string>() : "6",
            Definition = record.Properties.ContainsKey("definition") ? record.Properties["definition"].As<string>() : "",
            IdentificationSign = record.Properties.ContainsKey("identificationSign") ? record.Properties["identificationSign"].As<string>() : "",
            LearningTip = record.Properties.ContainsKey("learningTip") ? record.Properties["learningTip"].As<string>() : ""
        };
        return node;
    }

    public async ValueTask DisposeAsync()
    {
        await _driver.DisposeAsync();
    }
}
