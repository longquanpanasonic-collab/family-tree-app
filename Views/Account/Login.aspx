@{
    ViewBag.Title = "Đăng Nhập";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<div class="row justify-content-center">
    <div class="col-md-6">
        <div class="card shadow-custom">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0"><i class="fas fa-sign-in-alt"></i> Đăng Nhập</h4>
            </div>
            <div class="card-body p-5">
                <% using (Html.BeginForm("Login", "Account", FormMethod.Post, new { @class = "needs-validation", novalidate = "novalidate" })) { %>
                <%: Html.AntiForgeryToken() %>

                <% if (!ViewData.ModelState.IsValid) { %>
                <div class="alert alert-danger" role="alert">
                    <i class="fas fa-exclamation-circle"></i> Đăng nhập thất bại. Vui lòng kiểm tra lại.
                </div>
                <% } %>

                <div class="mb-3">
                    <label class="form-label"><i class="fas fa-user"></i> Tên Đăng Nhập</label>
                    <input type="text" class="form-control" name="username" required placeholder="Nhập tên đăng nhập" />
                </div>

                <div class="mb-3">
                    <label class="form-label"><i class="fas fa-lock"></i> Mật Khẩu</label>
                    <input type="password" class="form-control" name="password" required placeholder="Nhập mật khẩu" />
                </div>

                <div class="mb-3 form-check">
                    <input type="checkbox" class="form-check-input" id="rememberMe" name="rememberMe" />
                    <label class="form-check-label" for="rememberMe">Ghi nhớ đăng nhập</label>
                </div>

                <button type="submit" class="btn btn-primary w-100"><i class="fas fa-sign-in-alt"></i> Đăng Nhập</button>
                <% } %>

                <hr />
                <p class="text-center">
                    Chưa có tài khoản?
                    <a href="<%: Url.Action("Register", "Account") %>" class="btn btn-outline-primary btn-sm w-100 mt-2">Đăng ký tài khoản mới</a>
                </p>
            </div>
        </div>
    </div>
</div>