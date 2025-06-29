import { useState } from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import './App.css'
import TrainingWorkshop from "./pages/TrainingWorkshop";
import Booklet from "./pages/Booklet";
import Thanks from "./pages/Thanks";


function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<TrainingWorkshop />} />
        <Route path="/booklet" element={<Booklet />} />
        <Route path="/thanks" element={<Thanks />} />
      </Routes>
    </BrowserRouter>
    </>
  )
}

export default App
