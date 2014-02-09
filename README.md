ASP.NET Advanced Role-Based Management
======================================

This is an example project to accompany a blog post describing a somewhat more complex approach to managing application authorization by creating groups of roles, to which indicidual users may be added or removed. 

The objective is to use the `[Authorize]` attribute to establish fine-grained roles at the application level, and then create Groups to which these roles are added. In this way, groups of "premissions" may be configured, and then users can be assigned to one or more of these "role groups," making assignment of application roles potentially parallell real-world user organization roles.

This project builds upon the foundation created by another example, [ASP.NET Role-Based Security Example][3], covered in the article [Extending Identity Accounts and Implementing Role-Based Authentication in ASP.NET MVC 5][1]

You will need to enable Nuget Package Restore in Visual Studio in order to download and restore Nuget packages during build. If you are not sure how to do this, see [Keep Nuget Packages Out of Source Control with Nuget Package Manager Restore][2]

You will also need to run Entity Framework Migrations `Update-Database` command per the article. The migration files are included in the repo, so you will NOT need to `Enable-Migrations` or run `Add-Migration Init`. 

[1]: http://www.typecastexception.com/post/2013/11/11/Extending-Identity-Accounts-and-Implementing-Role-Based-Authentication-in-ASPNET-MVC-5.aspx "Extending Identity Accounts and Implementing Role-Based Authentication in ASP.NET MVC 5"

[2]: http://www.typecastexception.com/post/2013/11/10/Keep-Nuget-Packages-Out-of-Source-Control-with-Nuget-Package-Manager-Restore.aspx "Keep Nuget Packages Out of Source Control with Nuget Package Manager Restore"

[3]: https://github.com/xivSolutions/AspNetRoleBasedSecurityExample "ASP.NET Role-Based Security Example"
