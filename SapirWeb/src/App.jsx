import { useState } from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import './App.css'
import TrainingWorkshop from "./pages/TrainingWorkshop";
import Booklet from "./pages/Booklet";

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<TrainingWorkshop />} />
        <Route path="/booklet" element={<Booklet />} />
      </Routes>
    </BrowserRouter>
    </>
  )
}

export default App
