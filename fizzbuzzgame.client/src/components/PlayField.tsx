import { useState, useEffect } from 'react'
import { fetchData } from '../services/api.ts'
import '../styles/PlayField.css'

type PlayFieldProps = {
    id: number,
    question: number,
    timeLimit: number
}

export default function PlayField({ id, question, timeLimit }: PlayFieldProps) {
    const [currentQuestion, setCurrentQuestion] = useState<number>(0);
    const [inputValue, setInputValue] = useState<string>("");
    const [correctCount, setCorrectCount] = useState<number>(0);
    const [incorrectCount, setIncorrectCount] = useState<number>(0);
    const [timer, setTimer] = useState<number>(0);

    //load data into variable when it's available
    useEffect(() => {
        setCurrentQuestion(question);
    }, [question]);
    useEffect(() => {
        setTimer(timeLimit);
    }, [timeLimit]);

    //start timer when mounted
    useEffect(() => {
        const countdownInterval = setInterval(() => {
            setTimer((prevTime) => {
                if (prevTime < 1) {
                    clearInterval(countdownInterval);
                    sumbitAndFetchNextQuestion();
                    return 0;
                }
                return prevTime - 1;
            });
        }, 1000);

        return () => {
            clearInterval(countdownInterval);
        }
    }, [timer]);

    const handleChangeValue = (e: React.ChangeEvent<HTMLInputElement>) => {
        setInputValue(e.target.value);
    }

    const sumbitAndFetchNextQuestion = async (e?: React.FormEvent) => {
        e?.preventDefault();
        setInputValue("");

        const options = {
            method: "POST",
            body: {
                answer: inputValue
            }
        };
        const data = await fetchData(`attempts/${id}/answer`, options);
        if (data) {
            if (data.question.id == id) {
                if (data.isCorrect) {
                    setCorrectCount(correctCount + 1);
                } else {
                    setIncorrectCount(incorrectCount + 1);
                }

                setCurrentQuestion(data.question.question);
                setTimer(timeLimit);
            }
        }
    };

    return (
        <div>
            <strong>{currentQuestion}</strong>
            <span className="individual-time-left">This question time left: {timer}</span>
            <form className="answer-form" onSubmit={sumbitAndFetchNextQuestion}>
                <input className="input-answer"
                    type="text"
                    placeholder="Enter your answer here"
                    value={inputValue}
                    onChange={handleChangeValue}
                />
                <button type="submit">Submit</button>
            </form>
            <span className="correct-answer">Correct answer: {correctCount}</span>
            <span className="incorrect-answer">Incorrect answer: {incorrectCount}</span>
        </div>
    )
}