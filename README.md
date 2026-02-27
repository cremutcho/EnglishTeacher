# 🚀 EnglishTeacher API

API REST desenvolvida em .NET 8 para gerenciamento de alunos e professores.

O projeto foi construído com foco em boas práticas utilizadas no mercado, aplicando conceitos como Clean Architecture, separação de responsabilidades e princípios SOLID.

---

## 📚 Sobre o Projeto

A EnglishTeacher API é um sistema backend responsável pelo gerenciamento de alunos, permitindo:

- Cadastro de alunos
- Atualização de dados
- Consulta individual e listagem
- Exclusão lógica (Soft Delete)

Esta é a **Versão 2 (V2)** do projeto, com melhorias estruturais e organização profissional do repositório.

---

## 🏗 Arquitetura

O projeto segue separação em camadas:

- **Domain** → Entidades e regras de negócio
- **Application** → Serviços e DTOs
- **Infrastructure** → Persistência e DbContext
- **API** → Controllers e configuração da aplicação

Princípios aplicados:

- SOLID (com foco em SRP)
- Separação de responsabilidades
- Injeção de dependência
- Boas práticas com Entity Framework Core

---

## 🔥 Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger

---

## 🧠 Melhorias da Versão 2

- Implementação de Soft Delete completo
- Uso de AsNoTracking para otimização de consultas
- Refatoração da camada de serviços
- Padronização de DTOs
- Limpeza e organização profissional do repositório

---

## ▶️ Como Executar o Projeto

```bash
git clone https://github.com/cremutcho/EnglishTeacher.git
cd EnglishTeacher
dotnet restore
dotnet run
