# Finances API

PT-Br: Api desenvolvida para compra e venda de moedas como forma de investimento, no endpoint inicial você tem acesso a todas as moedas disponíveis para negociação com seus caminhos dispostos através do HATEOAS, onde você tem acesso ao endpoint de detalhes e de negociação.
Para realizar a compra ou venda é necessário estar com seu usuário criado já com um valor inicial na carteira de valor da moeda nativa (BRL), acesso as detalhes do usuário e compra só são possíveis após autenticação com JWT Token, que é também por onde é retirado o id das negociaçãos e liberação de acesso aos detalhes da carteira.

EN-us: Api developed for exchange of currencies as a form of investment, from the initial endpoint you'll have access to currencies available for exchange with their different URL's informed through HATEOAS, where you have access to details and exchange endpoint's.
In order to negotiate the currencies you'll have to be already logged in your account with initial deposit in the native currency (BRL), access to user's details and exchange are only possible after the authentication with JWT Token, wich is also from where the id for the details and exchanges is taken from.

## Technologies used:

* .NET 3.1
* Jwt Token
* Swagger for documentation
* Refit to consume public API
* Seeding service for automatic database data

### Commands to run application:

```bash
    * Clonar repositório / Clone repository
    git clone https://git.gft.com/vsav/financesapi

    * Entrar na pasta / Move to directory
    cd financesapi/

    * Baixar pacotes .NET / Download .NET packages
    dotnet restore

    * Enviar schema para banco de dados e popular o mesmo
    * Send schema to database and insert data
    dotnet database update

    * Rodar aplicação / Run Application
    dotnet watch run

    * Acessar em / Access in
    https://localhost:5001/
```

## User for checking API details:
### **Login:** vinicius@gft.com
#### **Password:** vinicius

## Endpoints:

### Login needed for access data and exchange
```
    POST - LOGIN
    /user/login
    Body:
    {
        "email": "email",
        "password": "password"
    }

    POST - CREATE
    /user/
    {
        "name": "name",
        "email": "email",
        "password": "password",
        "initialDeposit": 0.0000000
    }

    GET - GET USER INFORMATION AND WALLET
    /user/{id}
    
    AFTER LOGGED IN YOU'LL NEED TOKEN FOR FURTHER ACTIONS ON SYSTEM
```

### Get informations on currencies available for exchange
```
    GET - GET ALL CURRENCIES
    /

    POST - CREATE NEW CURRENCY
    /
    {
        "code": "code",
        "name": "name"
    }

    DELETE - DELETE CURRENCY
    /
    {
        "code": "code",
        "name": "name"
    }
```

### Exchange currency and get currency quote details
```
    GET - GET CURRENCY DETAILS BY CODE
    /details/{code}

    POST - EXCHANGE CURRENCY AND CREATE INVESTMENT IN USER'S WALLET
    /details/{code}
    BUY
    {
        "buyCurrency": 0
    }

    SELL
    {
        "sellCurrency": 0
    }
```

## Todo:
* Add encryption for password
* Add admin method for closing account converting all investments for native currency
* Add endpoint for makeing a deposit

### UML and example images:

![Screenshot](https://raw.githubusercontent.com/Vinicius-92/finances-api/main/Images/UML.jpg)
![Screenshot](https://raw.githubusercontent.com/Vinicius-92/finances-api/main/Images/UserInfo.jpg)
![Screenshot](https://raw.githubusercontent.com/Vinicius-92/finances-api/main/Images/Quote.jpg)





