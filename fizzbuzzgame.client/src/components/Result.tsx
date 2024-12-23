import { fetchData } from '../services/api.ts'
import { useState, useEffect } from 'react'

type ResultScore = {
    correctNumber: number,
    incorrectNumber: number
}

type ResultProps = {
    id: number
}

export default function Result({ id }: ResultProps) {
    const [result, setResult] = useState<ResultScore>();

    useEffect(() => {
        const fetchResult = async () => {
            const options = {
                method: "PUT",
                body: {}
            };
            const data = await fetchData(`attempts/${id}/finalize`, options);
            if (data) {
                setResult({
                    correctNumber: data.correctNumber,
                    incorrectNumber: data.incorrectNumber
                });
            }
        };

        fetchResult();
    }, []);

    return (
        <div className="results">
            <span className="correct-result">Correct answer: {result?.correctNumber}</span>
            <span className="incorrect-result">Incorrect answer: {result?.incorrectNumber}</span>
        </div>
    )
}