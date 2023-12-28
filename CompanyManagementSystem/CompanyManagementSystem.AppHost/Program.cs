IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CompanyManagementSystem_Web_Server>("CompanyManagementSystemAPI");

builder.Build().Run();