import { useState, useEffect } from 'react'
import { fetchData } from '../services/api.ts'
import { useNavigate } from 'react-router-dom'

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
                    SumbitAndFetchNextQuestion();
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

    const SumbitAndFetchNextQuestion = async (e?: React.FormEvent) => {
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
            //else if (data.question.id == 0) {
            //    //id == 0 means the game ended
            //    //const navigate = useNavigate();
            //    //navigate(`/play/${id}/result`, {
            //    //    state: { name, correctCount, incorrectCount }
            //    //});
            //}
        }
    };

    return (
        <div>
            <strong>{currentQuestion}</strong>
            <span>This question time left: {timer}</span>
            <form onSubmit={ SumbitAndFetchNextQuestion }>
                <input
                    type="text"
                    placeholder="Enter your answer here"
                    value={inputValue}
                    onChange={handleChangeValue}
                />
                <button type="submit">Submit</button>
            </form>
            <span>Correct answer: {correctCount}</span>
            <span>Incorrect answer: {incorrectCount}</span>
        </div>
    )
}