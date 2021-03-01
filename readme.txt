#Add appsettings.json file on webapi projetcs with this content
{
  "ConnectionStrings": {
    "WebApi001": "Server=localhost;DataBase=webapi001;Uid=[user];Pwd=[Password]"
  },
  "TokenConfiguration": {
    "Audience": "Audience123456789",
    "Issuer": "Issuer123456789",
    "Secret": "this is my custom Secret key for authnetication",
    "Minutes": 10,
    "Days": 1
  },
  "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
  },
  "AllowedHosts": "*"
 }
