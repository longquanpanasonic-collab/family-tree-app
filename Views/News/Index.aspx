@{
    ViewBag.Title = "Tin Tức";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h1><i class="fas fa-newspaper"></i> Quản Lý Tin Tức</h1>

<% if (Request.IsAuthenticated) { %>
<p class="mb-3">
    <a href="<%: Url.Action("Create", "News") %>" class="btn btn-success"><i class="fas fa-plus"></i> Tạo Tin Tức Mới</a>
</p>
<% } %>

<!-- Tìm Kiếm -->
<div class="card mb-4 shadow-custom">
    <div class="card-header bg-light">
        <h5><i class="fas fa-search"></i> Tìm Kiếm</h5>
    </div>
    <div class="card-body">
        <% using (Html.BeginForm("Index", "News", FormMethod.Get, new { @class = "row g-3" })) { %>
        <div class="col-md-6">
            <input type="text" name="searchTitle" class="form-control" placeholder="Tìm kiếm theo tiêu đề" value="<%: ViewBag.SearchTitle %>" />
        </div>
        <div class="col-md-4">
            <select name="category" class="form-select">
                <option value="">-- Tất Cả Danh Mục --</option>
                <% foreach (var cat in (ViewBag.Categories ?? new List<string>())) { %>
                <option value="<%: cat %>" <% if (ViewBag.SelectedCategory == cat) { %>selected<% } %>><%: cat %></option>
                <% } %>
            </select>
        </div>
        <div class="col-md-2">
            <button type="submit" class="btn btn-primary w-100"><i class="fas fa-search"></i> Tìm</button>
        </div>
        <% } %>
    </div>
</div>

<!-- Danh Sách Tin Tức -->
<div class="row"></div>