import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
// import '../src/pages/ReportsPage';
import ReportsPage from './pages/ReportsPage';

// function App() {
//   return (
//     <div style={{ padding: '2rem' }}>
//       <h1>Payment Reporting Dashboard</h1>
//       <p>Start building your features here!</p>
//     </div>
//   );
// }

function App() {
  return <ReportsPage />;
}

export default App
