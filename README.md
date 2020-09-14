# ASP.NET Core AWS Serverless WebAPI Clean Architecture Template

## Information and Origin

This Template originated from trying to create a easy-to-manage and up-to-date template to quickly get serverless web api's running on the AWS Lambda service.

This project was an attempt to merge my all time favorite clean architecture template from [jasontaylordev](https://github.com/jasontaylordev), who was a big inspiration to adapt the clean architecture mindset into all projects i have worked on since discovering his template.

The Template can be found here: [CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)

## Database Migrations

To create migrations run this command from the Api folder:

`dotnet ef migrations add InitialCreate --project ../Infrastructure/Infrastructure.csproj`

## Deployment

To deploy the WebAPI to the Lambda run this command from the Solution root:

`dotnet lambda deploy-serverless`