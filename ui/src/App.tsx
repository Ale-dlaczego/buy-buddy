import React from 'react';
import './App.css';

function App() {
  const TEMPLATE_VAR = window._env_.TEMPLATE_VAR;

  return (
    <div className="App">
      <header className="App-header">
        <div>
          zmienna: {TEMPLATE_VAR}
          <hr></hr>
          Edit <code>src/App.tsx</code> and save to reload.
        </div>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
