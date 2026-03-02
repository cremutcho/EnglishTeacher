# 🚀 EnglishTeacher API

API REST desenvolvida em .NET 8 para gerenciamento de alunos e professores.

O projeto foi construído com foco em boas práticas do mercado, aplicando conceitos como Clean Architecture, separação de responsabilidades e princípios SOLID.

---

## 📚 Sobre o Projeto

A **EnglishTeacher API** é um backend responsável pelo gerenciamento de alunos, permitindo:

- Cadastro de alunos e professores
- Atualização de dados
- Consulta individual e listagem
- Exclusão lógica (Soft Delete)
- **Autenticação JWT e controle de roles** (Admin, Teacher, Student)
- Endpoints protegidos por roles

Esta é a **Versão 3 (V3)** do projeto, incluindo melhorias em segurança e gerenciamento de acesso.

---

## 🏗 Arquitetura

O projeto segue a separação em camadas:

- **Domain** → Entidades e regras de negócio  
- **Application** → Serviços, DTOs e lógica de autenticação  
- **Infrastructure** → Persistência e DbContext  
- **API** → Controllers e configuração da aplicação  

### Princípios aplicados

- SOLID (com foco em SRP)  
- Separação de responsabilidades  
- Injeção de dependência  
- Boas práticas com Entity Framework Core  
- Autenticação e autorização seguras com JWT  

---

## 🔥 Tecnologias Utilizadas

- .NET 8  
- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- AutoMapper  
- Swagger (OpenAPI)  

---

## 🧠 Melhorias da Versão 3

- Autenticação JWT funcional  
- Controle de roles (Admin, Teacher, Student)  
- Endpoints protegidos por roles  
- Registro e login de usuários com validação de permissões  
- Refatoração de DTOs e serviços para suportar autenticação e roles  
- Limpeza e organização final do repositório  

---

## ▶️ Como Executar o Projeto

```bash
git clone https://github.com/cremutcho/EnglishTeacher.git
cd EnglishTeacher
dotnet restore
dotnet run
