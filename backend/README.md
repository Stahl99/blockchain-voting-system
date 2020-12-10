# Blockchain Voting System Backend

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
