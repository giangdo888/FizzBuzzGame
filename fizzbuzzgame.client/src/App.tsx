import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home.tsx'
import PlayGame from './pages/PlayGame.tsx'

export function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/play/:id" element={<PlayGame /> } />
            </Routes>
        </Router>
    )
}