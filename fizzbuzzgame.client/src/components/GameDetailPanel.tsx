import React from 'react';
import { useState, useEffect, useRef } from 'react'
import { useNavigate } from 'react-router-dom'
import '../styles/GameDetailPanel.css'

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
    closeModal: () => void
}

export default function GameDetailPanel({ id, name, minRange, maxRange, rules, closeModal }: DetailPanelProps) {
    const [durationInput, setDurationInput] = useState<string>("");
    const modalOverlayRef = useRef<HTMLDivElement | null>(null);

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

    // close the modal when click outside
    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (modalOverlayRef.current && modalOverlayRef.current === event.target) {
                closeModal();
            }
        };

        document.addEventListener('mousedown', handleClickOutside);

        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    return (
        <div className="game-detail-modal-overlay" ref={modalOverlayRef}>
            <div className="game-detail-modal-content">
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
                        placeholder="Enter game duration (minimum 60s)"
                    />
                    <button type="submit">Start game</button>
                </form>
            
            </div>
        </div>
    );
}