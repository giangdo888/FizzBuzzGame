import { useState } from 'react'
import { useNavigate } from 'react-router-dom'

export type Rule = {
    divisor: number;
    word: string;
}

interface DetailPanelProps {
    id: number;
    name: string;
    minRange: number;
    maxRange: number;
    rules: Rule[];
}

export default function GameDetailPanel({ id, name, minRange, maxRange, rules }: DetailPanelProps) {
    const [durationInput, setDurationInput] = useState<string>("");

    const handleChangeInput = (e: React.ChangeEvent<HTMLInputElement>) => {
        setDurationInput(e.target.value);
    }

    const navigate = useNavigate();
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        navigate(`/play/${id}`, {
            state: { durationInput, name }
        });
    };

    return (
        <div className="game-detail-panel">
            <h2>{name}</h2>
            <h3>Number range: {minRange} to {maxRange}</h3>
            <span>Score as many points as posible within the time limit by replacing divisors with corresponding words: </span>
            {rules.map((rule) => (
                <span key={rule.divisor}>{rule.divisor} - {rule.word}</span>
            ))}

            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={durationInput}
                    onChange={handleChangeInput}
                    placeholder="Enter game duration"
                />
                <button type="submit">Start game</button>
            </form>

        </div>
    );
}