# Web API для PostgreSQL
REST API, созданный на ASP .NET Core, для управления клиентами и их заказами.
## Функциональность
* Работа с PostgreSQL
* CRUD для таблиц клиентов и заказов
* Получение данных из таблиц клиентов и заказов с фильтрацией и пагинацией
* Dependency Injection
* Документация через Swagger
## Используемые технологии
* .NET 8.0
* ASP .NET Core 8.0
* Entity Framework Core 9.0.4
* Npgsql для работы с PostgreSQL
* Swagger
* Встроенный DI-контейнер ASP .NET Core
* xUnit тесты
## Запуск проекта
1. Добавьте или отредактируйте строку подключения к серверу PostgreSQL в файле ```appsettings.json```:
   ```json
   "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=port;Database=mydb;Username=myuser;Password=mypassword"
   }
   ```
2. Установите зависимости (если не установлены):
   ```bash
   dotnet restore
   ```
3. Примените миграции:
   ```bash
   dotnet ef database update
   ```
4. Запустите проект:
   ```bash
   dotnet run --project API
   ```
5. Откройте Swagger, если он не открылся автоматически:
   ```
   https://localhost:*порт*
   ```
## Поддерживаемые запросы
- Clients
  - ``` GET/api/Clients ``` - получение списка со всеми клиентами
  - ``` GET/api/Clients/{id} ``` - получение клиента по id
  - ``` GET/api/Clients/filtered ``` - получение списка клиентов по фильтру (с пагинацией)
  - ``` POST/api/Clients ``` - создание нового клиента
  - ``` PUT/api/Clients/{id} ``` - редактировние клиента по id
  - ``` DELETE/api/Clients/{id} ``` - удаление клиента по id
- Orders
  - ``` GET/api/Orders ``` - получение списка со всеми заказами (без информации о клиенте)
  - ``` GET/api/Orders/{id} ``` - получение заказа по id (с информацией о клиенте или без неё)
  - ``` GET/api/Orders/filtered ``` - получение списка заказов по фильтру (с пагинацией)
  - ``` GET/api/Orders/total-cost-of-birthday-orders ``` - получение списка сумм заказов со статусом «Выполнен» по каждому клиенту, произведенных в день рождения клиента
  - ``` GET/api/Orders/average-costs-by-hour ``` - получение списка часов в порядке убывания со средним чеком заказов за каждый час
  - ``` POST/api/Orders ``` - создание нового заказа
  - ``` PUT/api/Orders/{id} ``` - редактирование клиента по id
  - ``` DELETE/api/Clients/{id} ``` - удаление клиента по id
## Тесты
- Все методы контроллеров протестированы через Unit-тесты
