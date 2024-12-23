import { useState, useEffect, useRef } from 'react'
import { fetchData } from '../services/api.ts'
import '../styles/GameModal.css'

type Rule = {
    divisor: number,
    word: string
}

type FormData = {
    name: string,
    minRange: number,
    maxRange: number,
    rules: Rule[]
}

interface GameModalProps {
    closeModal: () => void
    onGameAdded: () => void
}

export default function GameModal({ closeModal, onGameAdded }: GameModalProps) {
    const modalOverlayRef = useRef<HTMLDivElement | null>(null);
    const [formData, setFormData] = useState<FormData>({
        name: "",
        minRange: 0,
        maxRange: 0,
        rules: [
            {
                divisor: 0,
                word: "",
            },
        ],
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>, index?: number) => {
        e.preventDefault();
        const { name, value } = e.target;

        if (index !== undefined) {
            // Updating a rule
            setFormData((prevData) => {
                const updatedRules = [...prevData.rules];
                updatedRules[index] = { ...updatedRules[index], [name]: value };
                return { ...prevData, rules: updatedRules };
            });
        } else {
            // Updating a top-level field
            setFormData((prevData) => ({
                ...prevData,
                [name]: value,
            }));
        }
    }

    const createNewGame = async (e: React.FormEvent) => {
        e.preventDefault();
        const options = {
            method: "POST",
            body: formData
        };
        await fetchData('games', options);
        onGameAdded();
        closeModal();
    }

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
        <div className="modal-overlay" ref={modalOverlayRef}>
            <div className="modal-content">
                <form onSubmit={createNewGame}>
                    <div>
                        <label>Name:</label>
                        <input
                            type="text"
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                            />
                    </div>
                    <div>
                        <label>Min range:</label>
                        <input
                            type="number"
                            name="minRange"
                            value={formData.minRange}
                            onChange={handleChange}
                        />
                    </div>
                    <div>
                        <label>Max range:</label>
                        <input
                            type="number"
                            name="maxRange"
                            value={formData.maxRange}
                            onChange={handleChange}
                        />
                    </div>
                    <div>
                        <h3>Rules</h3>
                        <div className="rules">
                        {formData.rules.map((rule : Rule, index: number) => (
                            <div key={index} className="rule">
                                <label>Divisor: </label>
                                <input
                                    type="number"
                                    name="divisor"
                                    value={rule.divisor}
                                    onChange={(e) => handleChange(e, index)}
                                />
                                <label>Word: </label>
                                <input
                                    type="text"
                                    name="word"
                                    value={rule.word}
                                    onChange={(e) => handleChange(e, index)}
                                />
                            </div>
                        ))}
                        </div>
                    </div>
                    <button type="button" onClick={() =>
                        setFormData((previousData) => ({
                            ...previousData,
                            rules: [...previousData.rules, { divisor: 0, word: "" }]
                        }))
                    }>
                    Add rule
                    </button>
                    <button type="submit">Create Game</button>
                </form>
            </div>
        </div>
    );
}