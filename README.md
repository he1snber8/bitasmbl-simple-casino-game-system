# Simple Casino Game System

## Description

Simple Casino Game System is a minimal real-time multiplayer casino platform designed for developers and enthusiasts to explore building interactive online gaming using SignalR. Players can create and join tables, play dynamically configured games, and receive prize payouts through integrated services.

## Tech Stack

- **SignalR:** Real-time bi-directional communication for all player interactions.
- **JWT Authentication:** Mocked user authentication for simplified identity management.
- **Microservices Architecture:** Segregated services for Auth, Wallet, Lobby, and Games to ensure modularity and security.
- **React:** Frontend to provide a responsive and interactive user interface.
- **External Configuration:** Loading available games and rules dynamically from a `games.json` file.

## Installation

> Note: This project follows a microservices approach. Each service can be run independently.

1. Clone the repository:
   ```
   git clone https://github.com/your-username/bitasmbl-simple-casino-game-system.git
   cd bitasmbl-simple-casino-game-system
   ```

2. Install dependencies for each service and the frontend (navigate into each service folder):
   ```
   npm install
   ```

3. Prepare `games.json` file in the Lobby service folder (sample file included).

4. Run each service (Auth, Wallet, Lobby, Game1) using your preferred method (e.g., `dotnet run`, `node server.js`, etc., depending on stack).

5. Launch the React frontend:
   ```
   npm start
   ```

## Requirements

- Real-time communication strictly via SignalR hubs; no polling or REST calls for player actions.
- The Lobby service is the only authorized service to call Wallet for payouts.
- Games and rules must be loaded dynamically from an external `games.json`.
- JWT authentication should use mocked users for simplicity.
- Prize payout after games: 95% to the winner, 5% retained as margin.

## Implementation Overview

1. **Auth Service:** Provides JWT authentication with user mocking to secure communication.
2. **Wallet Service:** Holds and manages player balances, only callable by Lobby.
3. **Lobby Service:** Manages player tables, game sessions, and orchestrates payouts post game.
4. **Game1 Service:** Implements the first casino game logic, loaded/configured via `games.json`.
5. **Frontend (React):** User interface for login, table management, gameplay, and live updates.
6. **Infrastructure:** Configurations for services deployment, networking, and environment variables.

## Usage Example

- Players authenticate using JWT tokens from Auth Service.
- Upon login, users join the Lobby and see available tables and games.
- Players create or join tables and start playing the selected game.
- Game results trigger payouts with the Wallet service through the Lobby.
- All actions and game states update in real-time via SignalR.

---

When you are done, submit the project from your profile: https://bitasmbl.com/home/profile