# Totally Not DHBW Truffle Project

## Installation instructions

### Contracts

1. Install Python 3 and Node LTS on your system.
2. Clone this repository.
3. In the repository directory, run `npm i` or `yarn`.
4. Run `npm start` or `yarn start`.
5. Run `npm run migrate` or `yarn migrate`.
6. Run `npm test` or `yarn test`.

> A GUI version of [Ganache](https://www.trufflesuite.com/ganache) also can be
> downloaded.

### Frontend

1. In the `/app` directory, run `npm i` or `yarn`.
2. In the `/app` directory, run `npm run dev` or `yarn dev`.

### Connect to frontend in browser

1. Install the [Metamask](https://metamask.io/) extension in your browser.
2. In the setup dialog, click _Import Wallet_.
3. As the _Seed Phrase_, enter the following:

   ```
   come craft limit group stock hollow front fantasy scare river animal settle
   ```

4. Enter any password and click import.
5. In the Metamask extension, click on the _Main Ethereum Network_ dropdown and
   select _Custom RPC_. Choose a network name and enter as RPC URL
   `http://localhost:7545`.
6. Now you can try to connect to the frontend, if running, at
   http://localhost:8080.
7. If you can see in that you own 100 DHBW Coins in the frontend, everything is
   set up correctly.

## Remote Development

### Requirements

- [Docker](https://www.docker.com/products/docker-desktop) (not Docker-Toolbox)
- [Visual Studio Code](https://code.visualstudio.com/)
  - Extension: [Remote Development](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.vscode-remote-extensionpack)

### Instructions

1. Clone this repository
2. Open directory in VS Code
3. Open commands (CTRL/⌘ + SHIFT + P)
4. Select: _Remote-Containers: Reopen in Container_
5. Wait until the build is finished
6. Open a terminal via Terminal -> New Terminal
7. Run `./run.sh`
8. In the open tmux session, run `yarn migrate`

You can now access the frontend at http://localhost:8080 or run `yarn test` in
the VS Code console. See
[Connect to frontend in browser](#connect-to-frontend-in-browser) on how to use
the frontend.
