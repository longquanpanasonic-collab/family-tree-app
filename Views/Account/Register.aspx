@{
    ViewBag.Title = "Đăng Ký";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<div class="row justify-content-center">
    <div class="col-md-6">
        <div class="card shadow-custom">
            <div class="card-header bg-success text-white">
                <h4 class="mb-0"><i class="fas fa-user-plus"></i> Đăng Ký Tài Khoản</h4>
            </div>
            <div class="card-body p-5">
                <% using (Html.BeginForm("Register", "Account", FormMethod.Post, new { @class = "needs-validation", novalidate = "novalidate" })) { %>
                <%: Html.AntiForgeryToken() %>

                <% if (!ViewData.ModelState.IsValid) { %>
                <div class="alert alert-danger" role="alert">
                    <i class="fas fa-exclamation-circle"></i> Đăng ký thất bại. Vui lòng kiểm tra lại.
                </div>
                <% } %>

                <div class="mb-3">
                    <label class="form-label"><i class="fas fa-user"></i> Tên Đăng Nhập</label>
                    <input type="text" class="form-control" name="username" required placeholder="Nhập tên đăng nhập (tối thiểu 3 ký tự)" />
                </div>

                <div class="mb-3">
                    <label class="form-label"><i class="fas fa-envelope"></i> Email</label>
                    <input type="email" class="form-control" name="email" required placeholder="Nhập email" />
                </div>

                <div class="mb-3">
                    <label class="form-label"><i class="fas fa-lock"></i> Mật Khẩu</label>
                    <input type="password" class="form-control" name="password" required placeholder="Nhập mật khẩu (tối thiểu 6 ký tự)" />
                </div>

                <div class="mb-3">
                    <label class="form-label"><i class="fas fa-lock"></i> Xác Nhận Mật Khẩu</label>
                    <input type="password" class="form-control" name="confirmPassword" required placeholder="Xác nhận mật khẩu" />
                </div>

                <div class="mb-3">
                    <label class="form-label"><i class="fas fa-id-badge"></i> Họ Tên Đầy Đủ</label>
                    <input type="text" class="form-control" name="fullName" placeholder="Nhập họ tên" />
                </div>

                <button type="submit" class="btn btn-success w-100 mb-3"><i class="fas fa-check"></i> Đăng Ký</button>
                <% } %>

                <p class="text-center">
                    Đã có tài khoản? <a href="<%: Url.Action("Login", "Account") %>">Đăng nhập</a>
                </p>
            </div>
        </div>
    </div>
</div>