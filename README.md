# FizzBuzzGame 🎯

FizzBuzz is a fun, interactive full-stack application where players join a game and answer a sequence of numbers with the correct FizzBuzz responses. The game includes a countdown timer and a scoreboard that displays correct and incorrect answers after each submission.

---

## 🚀 Features
- **Game Participation**: Players can join the FizzBuzz game and participate by submitting answers.
- **Create New Game**: Users can create a new game session, which includes setting up a countdown timer.
- **Real-time Answer Display**: After each answer submission, players see whether their response was correct or incorrect.
- **Leaderboard**: View the players' scores and rankings during and after the game.
- **Game Timer**: A countdown timer is implemented to add excitement and urgency to the game.

---

## 🛠️ Tech Stack

### Frontend
- **TypeScript**: A statically typed language that provides enhanced tooling and error-checking for the app.
- **React**: A powerful JavaScript library for building user interfaces, enabling a smooth user experience.
- **CSS**: Custom styles for the UI to ensure a polished and user-friendly design.
- **Vitest**: A fast unit testing framework for testing frontend components.

### Backend
- **ASP.NET Core**: The web framework for building the backend services, ensuring fast and scalable operations.
- **xUnit**: A unit testing framework for backend logic and functionality.
- **SignalR**: Real-time communication between clients and the server to handle live game updates.

### Database
- **PostgreSQL**: A robust relational database to store game data such as player scores, game sessions, and answers.

### Containerization
- The entire project is containerized using **Docker** for easy deployment and consistent environment setup across all platforms.

---

## 📋 Prerequisites

Before running the application, make sure you have the following installed:

- [**.NET SDK**](https://dotnet.microsoft.com/download)
- [**PostgreSQL**](https://www.postgresql.org/download/)
- [**Docker**](https://www.docker.com/get-started) (for running the app in containers)

---

## 🚀 How to Run the Application

1. **Clone the repository**:
- git clone https://github.com/giangdo888/FizzBuzzGame.git
- cd FizzBuzzGame

### 2. Set Up Docker Containers
   Docker Compose is used to set up and run the application’s containers for both frontend and backend.
- docker compose up --build -d

### 3. Access the Application
   Once the containers are up and running, you can access the ToDoList UI by navigating to:
- http://localhost:3000/
