# MedicalServicesManagement

Поиск > Консоль диспетчера пакетов. 

0) Установить EF tool
```
dotnet tool install --global dotnet-ef --version 9.0.12
```

1) Управление миграциями:

create:
```
dotnet ef migrations add InitialAuthDbCreate -c AuthDbContext --project src\MedicalServicesManagement.DAL\MedicalServicesManagement.DAL.csproj
```

```
dotnet ef migrations add InitialMedServiceCreate -c MedServiceContext --project src\MedicalServicesManagement.DAL\MedicalServicesManagement.DAL.csproj
```


use:
```
dotnet ef database update -c AuthDbContext --project src\MedicalServicesManagement.DAL\MedicalServicesManagement.DAL.csproj
```

```
dotnet ef database update -c MedServiceContext --project src\MedicalServicesManagement.DAL\MedicalServicesManagement.DAL.csproj
```

2) Применить sql-скрипты (Auth.sql, Med.sql)