# You Learn Geometry (YLG)

> **Nền tảng học tập trực tuyến Hình học phẳng THCS tích hợp Đồ thị tri thức (Knowledge Graph) & Cơ sở dữ liệu NoSQL đa mô hình (Polyglot Persistence).**

---

## 1. Giới thiệu dự án

**You Learn Geometry (YLG)** là hệ thống học tập tương tác trực quan dành cho học sinh Trung học cơ sở (từ Lớp 6 đến Lớp 9). Hệ thống số hóa và tổ chức toàn diện kiến thức hình học phẳng theo bộ Sách giáo khoa chuẩn (Kết nối tri thức & Chân trời sáng tạo), giải quyết rào cản tư duy trừu tượng của học sinh khi học chứng minh và nhận biết hình học.

### Điểm nổi bật của hệ thống
- **Kiến trúc NoSQL đa mô hình (Polyglot Persistence):**
  - **MongoDB Atlas (Document Database):** Lưu trữ hồ sơ người dùng, bài học, cây chương trình, tiến độ học tập, lịch sử kiểm tra và huy hiệu thành tích.
  - **Neo4j Aura (Graph Database):** Lưu trữ Đồ thị tri thức (Knowledge Graph) mô hình hóa cấu trúc quan hệ kế thừa (`IS_SPECIAL_CASE_OF`), thuộc tính (`HAS_PROPERTY`), và điều kiện tiên quyết (`REQUIRES`) giữa các khái niệm hình học phẳng.
- **Trải nghiệm học tập chuẩn Coursera:**
  - Sidebar điều hướng danh mục chủ đề phẳng bám lề trái, chuyển đổi chủ đề tức thì mà không cần tải lại trang.
  - Danh sách bài học bố trí theo chiều dọc, trực quan hóa trạng thái bằng các biểu tượng tròn tối giản (`✓` Đã hoàn thành, `●` Đang học, `○` Chưa học).
- **Phòng thí nghiệm hình học tương tác (Interactive Geometry Lab):** Tích hợp công cụ vẽ hình động GeoGebra giúp quan sát trực quan tính chất góc, cạnh, đường thẳng song song, đường tròn.
- **Công cụ lắp ghép tính chất (Property Builder):** Luyện tư duy chứng minh logic qua việc ghép các điều kiện giả thiết để suy ra hình học mục tiêu.
- **Hình học trong thực tiễn (Geometry Real World):** Nhận diện mô hình hình học từ ảnh vật thể thực tế (gạch bông, biển báo, cánh diều, bảng lớp, đồng hồ, cầu sắt...), hỗ trợ kéo thả đỉnh thủ công (Contour Edit) và phân loại hình học thời gian thực.
- **Đánh giá kiến thức 2 tầng:**
  - *Mini Test (4 câu trắc nghiệm)* sau mỗi bài học: Đạt từ 75% trở lên để xác nhận hoàn thành bài.
  - *Topic Test (10 câu trắc nghiệm)* tổng hợp chủ đề: Đạt từ 70% trở lên để nhận dấu tích hoàn thành toàn bộ chủ đề.

---

## 2. Thông tin tài khoản đăng nhập

Hệ thống đã được thiết lập sẵn tài khoản mẫu thông qua cơ chế tự động Seed dữ liệu khi khởi động:

| Vai trò | Tên đăng nhập (Username) | Email | Mật khẩu (Password) | Quyền hạn & Chức năng chính |
| :--- | :--- | :--- | :--- | :--- |
| **Quản trị viên (Admin)** | `admin` | `admin@ylg.edu.vn` | `admin` | Truy cập trang quản trị `/Admin`: Xem thống kê toàn hệ thống, quản lý tài khoản người dùng (khóa/mở khóa), thêm/sửa/xóa bài học, đồng bộ đồ thị tri thức sang Neo4j. |
| **Học sinh (Student)** | `longstudent` | `hocsinh@ylg.edu.vn` | `123` | Đăng nhập vào `/Dashboard`: Tham gia học 4 khối lớp (Lớp 6 - 9), làm Mini Test & Topic Test, xem Bản đồ tri thức hình học, duy trì chuỗi Streak và nhận Huy hiệu. |

> **Ghi chú:** Người học cũng có thể tự tạo tài khoản mới bất kỳ tại trang [Đăng ký tài khoản](http://localhost:5161/Account/Register) bằng cách điền Họ tên, Tên đăng nhập, Email, Mật khẩu và chọn khối lớp mong muốn.

---

## 3. Công nghệ sử dụng (Tech Stack)

| Thành phần | Công nghệ / Thư viện | Vai trò |
| :--- | :--- | :--- |
| **Backend** | ASP.NET Core 9.0 (C# 13) | Xây dựng API và Web Application theo mô hình MVC |
| **Document DB** | MongoDB Atlas (`MongoDB.Driver` v3.12) | Lưu trữ người dùng, tiến độ, bài học, kết quả thi |
| **Graph DB** | Neo4j Aura Cloud (`Neo4j.Driver` v6.3) | Lưu trữ và truy vấn đồ thị tri thức các khái niệm hình học |
| **Frontend** | Razor Pages, HTML5/CSS3 | Giao diện hiện đại, tối giản, responsive |
| **Graph Visualization** | Cytoscape.js | Trực quan hóa bản đồ mạng lưới hình học tương tác |
| **Math & Geometry Engine** | GeoGebra Web API | Mô phỏng hình học động |
| **Authentication** | ASP.NET Cookie Authentication | Quản lý phiên đăng nhập và phân quyền (Roles: Admin, Student) |

---

## 4. Hướng dẫn cài đặt và Khởi chạy

### Yêu cầu môi trường
- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) trở lên.
- Kết nối Internet (để ứng dụng kết nối tới cơ sở dữ liệu đám mây MongoDB Atlas và Neo4j Aura).

### Các bước khởi chạy

1. **Clone mã nguồn dự án:**
   ```bash
   git clone https://github.com/klongnguyen/You-Learn-Geometry.git
   cd You-Learn-Geometry
   ```

2. **Restore Packages và Biên dịch dự án:**
   ```bash
   dotnet restore
   dotnet build
   ```

3. **Khởi chạy ứng dụng:**
   ```bash
   dotnet run
   ```
   *Khi khởi chạy lần đầu, hệ thống sẽ tự động gọi `IDataSeeder` để nạp danh mục bài học chuẩn, tài khoản người dùng và xây dựng cấu trúc đồ thị trên Neo4j.*

4. **Truy cập ứng dụng:**
   Mở trình duyệt web và điều hướng tới:
   ```text
   http://localhost:5161
   ```

---

## 5. Cấu trúc chương trình học tập (22 Bài học chuẩn THCS)

Hệ thống số hóa đầy đủ chương trình hình học phẳng THCS gồm 4 khối lớp:

### Lớp 6: Hình học trực quan & Hình học cơ bản
- **Chủ đề 1: Một số hình phẳng trong thực tiễn**
  - Tam giác đều, Hình vuông, Lục giác đều
  - Hình chữ nhật, Hình thoi, Hình bình hành, Hình thang cân
  - Tính chu vi và diện tích các hình trong thực tiễn
  - Hình có trục đối xứng và Hình có tâm đối xứng
- **Chủ đề 2: Các hình hình học cơ bản**
  - Điểm và Đường thẳng
  - Điểm nằm giữa hai điểm, Tia, Đoạn thẳng và Độ dài đoạn thẳng
  - Góc, Các loại góc và Số đo góc

### Lớp 7: Góc và Tam giác
- **Chủ đề 1: Góc và Định lý**
  - Các góc ở vị trí đặc biệt (kề bù, so le trong, đồng vị)
  - Tia phân giác của một góc
  - Hai đường thẳng song song và Tiên đề Euclid
  - Định lý và chứng minh định lý hình học
- **Chủ đề 2: Tam giác bằng nhau & Tam giác đặc biệt**
  - Tổng các góc trong một tam giác
  - Ba trường hợp bằng nhau của tam giác (c-c-c, c-g-c, g-c-g)
  - Tam giác cân, Tam giác đều và Đường trung trực
  - Quan hệ giữa cạnh và góc đối diện, đường vuông góc và đường xiên

### Lớp 8: Tứ giác & Định lý hình học nâng cao
- **Chủ đề 1: Tứ giác**
  - Tứ giác và định lý tổng các góc của tứ giác
  - Hình thang cân và dấu hiệu nhận biết
  - Hình bình hành và tính chất hai đường chéo
  - Hình chữ nhật và ứng dụng trong tam giác vuông
  - Hình thoi và dấu hiệu hai đường chéo vuông góc
  - Hình vuông (sự kết hợp của hình chữ nhật và hình thoi)
- **Chủ đề 2: Định lý Thalès & Tam giác đồng dạng**
  - Định lý Thalès trong tam giác (thuận và đảo)
  - Tính chất đường phân giác trong tam giác
  - Các trường hợp đồng dạng của tam giác và ứng dụng thực tiễn
  - Định lý Pythagore và ứng dụng tính độ dài

### Lớp 9: Đường tròn & Hệ thức lượng
- **Chủ đề 1: Đường tròn**
  - Khái niệm đường tròn, dây cung và khoảng cách từ tâm đến dây
  - Vị trí tương đối của đường thẳng và đường tròn, tiếp tuyến của đường tròn
  - Góc ở tâm, góc nội tiếp và góc tạo bởi tia tiếp tuyến và dây cung
  - Tứ giác nội tiếp đường tròn và điều kiện nội tiếp
- **Chủ đề 2: Hệ thức lượng trong tam giác vuông**
  - Một số hệ thức về cạnh và đường cao trong tam giác vuông
  - Tỉ số lượng giác của góc nhọn (sin, cos, tan, cot)
  - Hệ thức giữa các cạnh và góc của tam giác vuông

---

## 6. Hướng dẫn sử dụng các phân hệ chức năng

### 6.1. Dành cho Học sinh (Student)
1. **Trang chủ & Bảng điều khiển cá nhân (`/Dashboard`):**
   - Theo dõi chuỗi ngày học liên tục (Streak Count).
   - Xem tổng số bài học đã hoàn thành và danh sách các Huy hiệu đã đạt được.
   - Nhấn nút **"Tiếp tục học"** để chuyển ngay tới bài học đang dở dang.
2. **Trang Chương trình & Bài học (`/Curriculum`):**
   - Chọn khối lớp cần học (Lớp 6, 7, 8, hoặc 9) từ menu trên đầu thanh sidebar.
   - Chọn từng chủ đề ở thanh sidebar bên trái.
   - Theo dõi trạng thái từng bài học qua ô tròn phía trước tiêu đề:
     - `✓` (Màu xanh lá): Bài học đã hoàn thành (đã làm bài kiểm tra đạt kết quả tốt).
     - `●` (Màu xanh dương): Bài học đang học hoặc đang chờ làm bài Mini Test.
     - `○` (Vòng tròn rỗng xám): Bài học chưa học.
   - Nhấn **"Học ngay →"** hoặc **"Ôn tập"** để mở chi tiết bài học.
   - Ở cuối mỗi chủ đề là thẻ **"Bài kiểm tra chủ đề (10 câu)"**. Khi hoàn thành tất cả bài học trong chủ đề, nút làm kiểm tra sẽ mở khóa.
3. **Chi tiết bài học & Thực hành (`/Lesson/Detail/{id}`):**
   - Đọc nội dung lý thuyết, bảng tính chất, công thức và ví dụ giải chi tiết.
   - Trải nghiệm phòng thí nghiệm hình học động GeoGebra.
   - Nhấn nút **"Bắt đầu làm Mini Test (4 câu)"** ở cuối bài để kiểm tra độ hiểu bài ngay lập tức.
4. **Bản đồ tri thức hình học (`/KnowledgeMap`):**
   - Khám phá mạng lưới liên kết giữa các hình (Ví dụ: Hình thang $\rightarrow$ Hình bình hành $\rightarrow$ Hình chữ nhật/Hình thoi $\rightarrow$ Hình vuông).
   - Lọc theo khối lớp (Lớp 6 hoặc Lớp 8).
   - Nhấp vào từng node để mở bảng thông tin chi tiết: Định nghĩa, Dấu hiệu nhận biết, và Mẹo ghi nhớ tính chất kế thừa.
5. **Công cụ lắp ghép tính chất (`/PropertyBuilder`):**
   - Chọn các điều kiện giả thiết và thuộc tính của hình học để quan sát kết luận suy diễn logic tương ứng.
6. **Hình học trong thực tiễn (Geometry Real World - `/RealWorld`):**
   - Chọn vật thể thực tế mẫu trong thư viện hoặc tải ảnh bất kỳ từ máy tính lên.
   - Kéo thả các đỉnh tròn trên Canvas để khớp chính xác với góc cạnh của vật thể trong ảnh (hỗ trợ chế độ Đa giác và Hình tròn).
   - Hệ thống tự động đo góc, độ dài cạnh, chu vi, diện tích và suy luận phân loại hình học (Hình vuông, Hình chữ nhật, Hình thoi, Hình thang cân, Tam giác đều, Hình tròn...).
   - Bấm nút **"Học bài này ngay →"** để chuyển thẳng tới bài giảng tương ứng trong chương trình.

### 6.2. Dành cho Quản trị viên (Admin)
1. **Thống kê tổng quan (`/Admin`):**
   - Xem tổng số người dùng, tổng số bài học, số bài học đã hoàn thành, tỷ lệ hoàn thành trung bình và điểm kiểm tra bình quân.
2. **Quản lý người dùng (`/Admin/Users`):**
   - Danh sách toàn bộ tài khoản học sinh trong hệ thống.
   - Chức năng **Khóa tài khoản / Kích hoạt lại tài khoản** chỉ với 1 click.
3. **Quản lý danh mục bài học (`/Admin/Lessons`):**
   - Thêm bài học mới hoặc sửa đổi nội dung bài giảng, mã bài học, khối lớp và chủ đề.
   - Xóa bài học khỏi MongoDB và tự động gỡ bỏ/cập nhật quan hệ tương ứng trên đồ thị tri thức Neo4j.

---

## 7. Cấu trúc thư mục dự án

```text
YouLearnGeometry/
├── Controllers/                 # Điều khiển luồng nghiệp vụ MVC
│   ├── AccountController.cs        # Đăng nhập, đăng ký, đăng xuất, profile
│   ├── AdminController.cs          # Quản trị hệ thống, quản lý người dùng, bài học
│   ├── AssessmentController.cs     # Xử lý bài thi Mini Test và Topic Test
│   ├── CurriculumController.cs     # Hiển thị lộ trình và danh mục bài học Coursera-style
│   ├── DashboardController.cs      # Bảng điều khiển học tập cá nhân của học sinh
│   ├── GeometryLabController.cs    # Phòng thí nghiệm hình học GeoGebra
│   ├── HomeController.cs           # Landing page giới thiệu
│   ├── KnowledgeMapController.cs   # Bản đồ đồ thị tri thức Neo4j & Cytoscape
│   ├── LessonController.cs         # Chi tiết bài học, lý thuyết và câu hỏi mini test
│   ├── PropertyBuilderController.cs# Công cụ xây dựng và suy luận tính chất hình học
│   └── RealWorldController.cs      # Phân tích hình học thực tế, canvas tương tác & preset
├── Models/                      # Mô hình thực thể dữ liệu (MongoDB & Neo4j)
│   ├── User.cs                     # Thực thể tài khoản người dùng
│   ├── Lesson.cs                   # Thực thể bài học hình học
│   ├── CurriculumLevel.cs          # Thực thể khối lớp và chủ đề
│   ├── KnowledgeGraphModels.cs     # Thực thể Node, Edge và DTO Cytoscape
│   └── ProgressAndAssessmentModels.cs # Thực thể tiến độ và bài kiểm tra
├── Services/                    # Dịch vụ kết nối cơ sở dữ liệu và nghiệp vụ
│   ├── MongoDbService.cs           # Khởi tạo kết nối & collections MongoDB Atlas
│   ├── Neo4jService.cs             # Kết nối driver Neo4j & truy vấn Cypher Graph
│   ├── DataSeeder.cs               # Tự động nạp dữ liệu người dùng, bài học & đồ thị
│   ├── DataSeeder.Lop6.cs          # Chi tiết dữ liệu 7 bài học Lớp 6
│   ├── DataSeeder.Lop7.cs          # Chi tiết dữ liệu 5 bài học Lớp 7
│   ├── DataSeeder.Lop8.cs          # Chi tiết dữ liệu 5 bài học Lớp 8
│   └── DataSeeder.Lop9.cs          # Chi tiết dữ liệu 5 bài học Lớp 9
├── ViewModels/                  # ViewModels phục vụ hiển thị dữ liệu ra Views
│   ├── AccountViewModels.cs
│   ├── AdminViewModels.cs
│   ├── RealWorldViewModels.cs      # DTO tọa độ điểm, mẫu thực tế và kết quả phân tích
│   └── StudentViewModels.cs
├── Views/                       # Giao diện người dùng Razor Views
│   ├── Account/                    # Trang đăng nhập, đăng ký
│   ├── Admin/                      # Giao diện quản trị viên
│   ├── Curriculum/                 # Giao diện chương trình học chuẩn Coursera
│   ├── Dashboard/                  # Giao diện tổng quan cá nhân
│   ├── KnowledgeMap/               # Giao diện bản đồ tri thức tương tác
│   ├── Lesson/                     # Giao diện chi tiết bài học
│   └── Shared/                     # Layout chung (_Layout.cshtml)
├── ToanTHCS/                    # Giáo trình Toán THCS tham khảo (PDF từ Lớp 6 - 9)
├── wwwroot/                     # Tài nguyên tĩnh (CSS, JS, hình ảnh, GeoGebra scripts)
├── appsettings.json             # Cấu hình chuỗi kết nối MongoDB Atlas & Neo4j Aura
├── Program.cs                   # Điểm khởi chạy ứng dụng & Dependency Injection
└── YouLearnGeometry.csproj      # File cấu hình dự án .NET 9
```

---

## 8. Tác giả & Bản quyền
- **Môn học:** Cơ sở dữ liệu NoSQL (Học kỳ 1 - 2026).
- **Nhóm thực hiện:** Nhóm 02.
- **Giảng viên hướng dẫn:** Thầy Bình.
- **Kho lưu trữ GitHub:** [You-Learn-Geometry](https://github.com/klongnguyen/You-Learn-Geometry)