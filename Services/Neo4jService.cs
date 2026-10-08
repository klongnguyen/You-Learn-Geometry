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

                    string color = type switch
                    {
                        "Shape" => "#3b82f6",     // Blue
                        "Property" => "#10b981",  // Green
                        "Concept" => "#f59e0b",   // Amber
                        _ => "#8b5cf6"            // Purple
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

        // Check if graph already has nodes
        var checkCursor = await session.RunAsync("MATCH (n) RETURN count(n) AS cnt");
        await checkCursor.FetchAsync();
        long count = checkCursor.Current["cnt"].As<long>();
        if (count > 0) return; // Already seeded

        // Seed comprehensive Knowledge Graph for Geometry
        string seedQuery = @"
            CREATE (hv:Shape {
                id: 'HinhVuong',
                label: 'Hình vuông',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có 4 góc vuông và 4 cạnh bằng nhau.',
                identificationSign: 'Hình chữ nhật có 2 cạnh kề bằng nhau, hoặc 2 đường chéo vuông góc. Hoặc hình thoi có 1 góc vuông, hoặc 2 đường chéo bằng nhau.',
                learningTip: 'Hình vuông là trường hợp ĐẶC BIỆT NHẤT: nó vừa là hình chữ nhật, vừa là hình thoi, và vừa là hình bình hành. Kế thừa toàn bộ tính chất của tất cả các hình này!'
            })
            CREATE (hcn:Shape {
                id: 'HinhChuNhat',
                label: 'Hình chữ nhật',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có 4 góc vuông.',
                identificationSign: 'Hình bình hành có một góc vuông hoặc có hai đường chéo bằng nhau. Hoặc hình thang cân có một góc vuông.',
                learningTip: 'Kế thừa toàn bộ tính chất của Hình bình hành, cộng thêm điểm nổi bật: 2 đường chéo vừa bằng nhau vừa cắt nhau tại trung điểm mỗi đường.'
            })
            CREATE (ht:Shape {
                id: 'HinhThoi',
                label: 'Hình thoi',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có 4 cạnh bằng nhau.',
                identificationSign: 'Hình bình hành có hai cạnh kề bằng nhau, hoặc có hai đường chéo vuông góc với nhau, hoặc có một đường chéo là đường phân giác của một góc.',
                learningTip: 'Kế thừa toàn bộ tính chất của Hình bình hành, đặc trưng: 2 đường chéo vuông góc với nhau và là các đường phân giác của các góc.'
            })
            CREATE (hbh:Shape {
                id: 'HinhBinhHanh',
                label: 'Hình bình hành',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có các cạnh đối song song và bằng nhau.',
                identificationSign: 'Tứ giác có các cạnh đối song song; hoặc các cạnh đối bằng nhau; hoặc 2 cạnh đối song song và bằng nhau; hoặc các góc đối bằng nhau; hoặc 2 đường chéo cắt nhau tại trung điểm mỗi đường.',
                learningTip: 'Là gốc rễ trực tiếp để phát triển lên Hình chữ nhật và Hình thoi. Mọi tính chất của hình bình hành đều đúng với hình chữ nhật, hình thoi và hình vuông!'
            })
            CREATE (htc:Shape {
                id: 'HinhThangCan',
                label: 'Hình thang cân',
                type: 'Shape',
                minGrade: '6',
                definition: 'Hình thang có hai góc kề một đáy bằng nhau (hoặc hai đường chéo bằng nhau).',
                identificationSign: 'Hình thang có 2 góc kề một đáy bằng nhau; hoặc hình thang có 2 đường chéo bằng nhau.',
                learningTip: 'Hình thang cân có tính đối xứng trục; hai đường chéo bằng nhau và hai cạnh bên bằng nhau.'
            })
            CREATE (hth:Shape {
                id: 'HinhThang',
                label: 'Hình thang',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tứ giác có hai cạnh đối song song.',
                identificationSign: 'Tứ giác có 1 cặp cạnh đối song song.',
                learningTip: 'Cặp cạnh song song gọi là hai đáy, khoảng cách giữa 2 đáy là chiều cao.'
            })
            CREATE (tg:Shape {
                id: 'HinhTuGiac',
                label: 'Tứ giác lồi',
                type: 'Shape',
                minGrade: '8',
                definition: 'Đa giác có 4 cạnh, luôn nằm trong cùng một nửa mặt phẳng có bờ là đường thẳng chứa bất kỳ cạnh nào.',
                identificationSign: 'Định lý: Tổng bốn góc của một tứ giác luôn bằng 360 độ.',
                learningTip: 'Tất cả các hình thang, hình bình hành, chữ nhật, thoi, vuông đều là các trường hợp đặc biệt của Tứ giác lồi!'
            })
            CREATE (tamGiac:Shape {
                id: 'HinhTamGiac',
                label: 'Hình tam giác',
                type: 'Shape',
                minGrade: '6',
                definition: 'Hình gồm ba đoạn thẳng nối ba điểm không thẳng hàng.',
                identificationSign: 'Tổng ba góc trong tam giác bằng 180 độ.',
                learningTip: 'Mọi tứ giác đều có thể chia thành hai hình tam giác bằng một đường chéo.'
            })
            CREATE (tron:Shape {
                id: 'HinhTron',
                label: 'Hình tròn',
                type: 'Shape',
                minGrade: '6',
                definition: 'Tập hợp tất cả các điểm cách tâm O một khoảng bằng bán kính R.',
                identificationSign: 'Có tâm đối xứng và vô số trục đối xứng đi qua tâm.',
                learningTip: 'Đường bao khép kín có độ cong đều, tính chất đặc biệt đối xứng hoàn hảo qua tâm.'
            })

            // Relationships
            CREATE (hv)-[:IS_SPECIAL_CASE_OF {label: 'là trường hợp đặc biệt của', explanation: 'Hình vuông thỏa mãn đầy đủ định nghĩa và tính chất của Hình chữ nhật (có 4 góc vuông)'}]->(hcn)
            CREATE (hv)-[:IS_SPECIAL_CASE_OF {label: 'là trường hợp đặc biệt của', explanation: 'Hình vuông thỏa mãn đầy đủ định nghĩa và tính chất của Hình thoi (có 4 cạnh bằng nhau)'}]->(ht)
            CREATE (hcn)-[:IS_SPECIAL_CASE_OF {label: 'là trường hợp đặc biệt của', explanation: 'Hình chữ nhật có 2 cặp cạnh đối song song và bằng nhau nên là Hình bình hành'}]->(hbh)
            CREATE (ht)-[:IS_SPECIAL_CASE_OF {label: 'là trường hợp đặc biệt của', explanation: 'Hình thoi có 2 cặp cạnh đối song song và bằng nhau nên là Hình bình hành'}]->(hbh)
            CREATE (hbh)-[:IS_SPECIAL_CASE_OF {label: 'là trường hợp đặc biệt của', explanation: 'Hình bình hành có 2 cạnh đối song song nên là Hình thang'}]->(hth)
            CREATE (htc)-[:IS_SPECIAL_CASE_OF {label: 'là trường hợp đặc biệt của', explanation: 'Hình thang cân có 2 cạnh đáy song song nên là Hình thang'}]->(hth)
            CREATE (hth)-[:IS_SPECIAL_CASE_OF {label: 'thuộc lớp', explanation: 'Hình thang là một dạng tứ giác có thêm điều kiện hai đáy song song'}]->(tg)
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
