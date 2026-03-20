# 🚀 EnglishTeacher API

API REST desenvolvida em **.NET 8** para uma plataforma de ensino de inglês, inspirada em apps como Duolingo.

O projeto evoluiu de um CRUD simples para um backend completo com **analytics, sessões de aprendizado e controle de progresso**.

---

## 📚 Sobre o Projeto

A **EnglishTeacher API** gerencia o fluxo de aprendizado de alunos, incluindo:

- Cadastro de alunos e professores  
- Gerenciamento de aulas e exercícios  
- Sessões de estudo (Learning Sessions)  
- Cálculo de desempenho do aluno  
- Analytics de aprendizado  
- Ranking (Leaderboard)  

---

## 🔥 Tecnologias Utilizadas

- .NET 8  
- ASP.NET Core Web API  
- Entity Framework Core  
- PostgreSQL  
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

### 🧠 Learning Sessions
- Iniciar sessão de estudo  
- Responder exercícios  
- Registro de acertos  
- Finalização com cálculo de score  

---

## 📊 Analytics

### Student Statistics
```http
GET /api/students/{id}/statistics
Leaderboard
GET /api/students/leaderboard
Learning History
GET /api/students/{id}/learning-history
Exemplo de resposta (Statistics)
{
  "student": "Andre",
  "studySessions": 12,
  "lessonsCompleted": 5,
  "totalExercises": 140,
  "correctAnswers": 112,
  "accuracy": 80
}
▶️ Como Executar o Projeto Localmente
git clone https://github.com/cremutcho/EnglishTeacher.git
cd EnglishTeacher
dotnet restore
dotnet run

Acesse o Swagger local:
http://localhost:{porta}/swagger

🌐 Deploy em Produção

A API está disponível online em:
EnglishTeacher API - Render