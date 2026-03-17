# 🚀 EnglishTeacher API

API REST desenvolvida em **.NET 8** para uma plataforma de ensino de inglês, inspirada em aplicações como Duolingo.

O projeto evoluiu de um CRUD simples para um backend completo com **analytics, sessões de aprendizado e controle de progresso**.

---

## 📚 Sobre o Projeto

A **EnglishTeacher API** é responsável por gerenciar o fluxo de aprendizado de alunos, incluindo:

- Cadastro de alunos e professores
- Gerenciamento de aulas e exercícios
- Sessões de estudo (Learning Sessions)
- Cálculo de desempenho do aluno
- Analytics de aprendizado
- Ranking (Leaderboard)

---

## 🏗 Arquitetura

O projeto segue uma arquitetura em camadas:

- **Domain** → Entidades e regras de negócio  
- **Application** → DTOs e lógica da aplicação  
- **Infrastructure** → Persistência e DbContext  
- **API** → Controllers e endpoints  

### Princípios aplicados

- SOLID  
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
- Swagger (OpenAPI)  

---

## 📦 Funcionalidades

### 👨‍🎓 Students
- CRUD completo  
- Acompanhamento de progresso  

### 👨‍🏫 Teachers
- CRUD completo  

### 📚 Lessons
- Gerenciamento de aulas  

### 📝 Exercises
- Exercícios com validação de resposta  

---

## 🧠 Learning Sessions

Fluxo completo de aprendizado:

- Iniciar sessão de estudo  
- Responder exercícios  
- Registro de acertos  
- Finalização com cálculo de score  

### Endpoints
