@{
    ViewBag.Title = "Trang Chủ";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<div class="row mb-5">
    <div class="col-md-12">
        <div class="jumbotron bg-light py-5 rounded">
            <h1 class="display-4"><i class="fas fa-tree text-primary"></i> Hệ Thống Quản Lý Gia Phả</h1>
            <p class="lead">Lưu giữ, chia sẻ và kết nối lịch sử gia đình của bạn trực tuyến</p>
            <hr class="my-4">
            <% if (!Request.IsAuthenticated) { %>
            <a class="btn btn-primary btn-lg" href="<%: Url.Action("Register", "Account") %>" role="button"><i class="fas fa-user-plus"></i> Đăng Ký Ngay</a>
            <a class="btn btn-outline-primary btn-lg" href="<%: Url.Action("Login", "Account") %>" role="button"><i class="fas fa-sign-in-alt"></i> Đăng Nhập</a>
            <% } %>
        </div>
    </div>
</div>

<!-- Thống Kê -->
<div class="row mb-5">
    <div class="col-md-4">
        <div class="card text-center border-0 shadow-custom">
            <div class="card-body">
                <h3 class="text-primary"><i class="fas fa-users"></i></h3>
                <h5 class="card-title">Thành Viên</h5>
                <p class="display-5 text-primary"><%: ViewBag.TotalMembers %></p>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card text-center border-0 shadow-custom">
            <div class="card-body">
                <h3 class="text-info"><i class="fas fa-newspaper"></i></h3>
                <h5 class="card-title">Tin Tức</h5>
                <p class="display-5 text-info"><%: ViewBag.TotalNews %></p>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card text-center border-0 shadow-custom">
            <div class="card-body">
                <h3 class="text-success"><i class="fas fa-images"></i></h3>
                <h5 class="card-title">Hình Ảnh</h5>
                <p class="display-5 text-success"><%: ViewBag.TotalGalleryImages %></p>
            </div>
        </div>
    </div>
</div>

<!-- Tin Nổi Bật -->
<% if (ViewBag.FeaturedNews != null && ViewBag.FeaturedNews.Count > 0) { %>
<h2 class="mb-4"><i class="fas fa-star text-warning"></i> Tin Nổi Bật</h2>
<div class="row mb-5">
    <% foreach (var news in ViewBag.FeaturedNews) { %>
    <div class="col-md-4 mb-4">
        <div class="card h-100 featured-news">
            <% if (!string.IsNullOrEmpty(news.FeaturedImage)) { %>
            <img src="~/Uploads/News/<%: news.FeaturedImage %>" class="card-img-top" alt="<%: news.Title %>">
            <% } %>
            <div class="card-body">
                <span class="badge bg-danger"><i class="fas fa-star"></i> Nổi Bật</span>
                <h5 class="card-title mt-2"><%: news.Title %></h5>
                <p class="card-text"><%: news.Content.Length > 100 ? news.Content.Substring(0, 100) + "..." : news.Content %></p>
                <small class="text-muted"><i class="fas fa-calendar"></i> <%: news.CreatedDate.ToString("dd/MM/yyyy") %></small>
            </div>
            <div class="card-footer bg-white border-0">
                <a href="<%: Url.Action("Details", "News", new { id = news.Id }) %>" class="btn btn-primary btn-sm">Xem Chi Tiết</a>
            </div>
        </div>
    </div>
    <% } %>
</div>
<% } %>

<!-- Features -->
<div class="row mt-5 pt-5 border-top">
    <h2 class="mb-4">Tính Năng Chính</h2>
    <div class="col-md-6 mb-4">
        <div class="d-flex">
            <div class="flex-shrink-0">
                <div class="display-4 text-primary"><i class="fas fa-sitemap"></i></div>
            </div>
            <div class="flex-grow-1 ms-3">
                <h5>Quản Lý Gia Phả</h5>
                <p>Thêm, chỉnh sửa và quản lý hàng trăm thành viên gia đình của bạn.</p>
            </div>
        </div>
    </div>
    <div class="col-md-6 mb-4">
        <div class="d-flex">
            <div class="flex-shrink-0">
                <div class="display-4 text-info"><i class="fas fa-newspaper"></i></div>
            </div>
            <div class="flex-grow-1 ms-3">
                <h5>Chia Sẻ Tin Tức</h5>
                <p>Chia sẻ các sự kiện, mốc của gia đình với các thành viên khác.</p>
            </div>
        </div>
    </div>
    <div class="col-md-6 mb-4">
        <div class="d-flex">
            <div class="flex-shrink-0">
                <div class="display-4 text-success"><i class="fas fa-images"></i></div>
            </div>
            <div class="flex-grow-1 ms-3">
                <h5>Thư Viện Ảnh</h5>
                <p>Lưu trữ và chia sẻ những bức ảnh đáng nhớ của gia đình.</p>
            </div>
        </div>
    </div>
    <div class="col-md-6 mb-4">
        <div class="d-flex">
            <div class="flex-shrink-0">
                <div class="display-4 text-warning"><i class="fas fa-search"></i></div>
            </div>
            <div class="flex-grow-1 ms-3">
                <h5>Tìm Kiếm Nâng Cao</h5>
                <p>Tìm kiếm nhanh chóng thành viên theo tên, giới tính, năm sinh.</p>
            </div>
        </div>
    </div>
</div>