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
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchResult = async () => {
            try {
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
            } catch (err) {
                setError('Failed to fetch result.');
            }
        };

        fetchResult();
    }, []);

    if(error) {
        return <div>{error}</div>;
    }

    if (!result) {
        return null;
    }

    return (
        <div className="results">
            <span className="correct-result">Correct answer: {result?.correctNumber}</span>
            <span className="incorrect-result">Incorrect answer: {result?.incorrectNumber}</span>
        </div>
    )
}