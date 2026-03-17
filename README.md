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


POST /api/learningsessions/start
POST /api/learningsessions/answer
POST /api/learningsessions/finish
GET /api/learningsessions/{id}
GET /api/learningsessions/student/{studentId}


---

## 📊 Analytics

### 📈 Student Statistics

- Total de sessões de estudo  
- Total de exercícios  
- Respostas corretas  
- Taxa de acerto (%)  
- Lições concluídas  


GET /api/students/{id}/statistics


---

### 🥇 Leaderboard

Ranking global dos alunos baseado em desempenho:


GET /api/students/leaderboard


---

### 📚 Learning History

Histórico completo de aprendizado do aluno:


GET /api/students/{id}/learning-history


---

## 📊 Exemplo de resposta (Statistics)

```json
{
  "student": "Andre",
  "studySessions": 12,
  "lessonsCompleted": 5,
  "totalExercises": 140,
  "correctAnswers": 112,
  "accuracy": 80
}
▶️ Como Executar o Projeto
git clone https://github.com/cremutcho/EnglishTeacher.git
cd EnglishTeacher
dotnet restore
dotnet run

Acesse o Swagger:

https://localhost:{porta}/swagger
