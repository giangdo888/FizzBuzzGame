import { useState, useEffect } from 'react';
import { useParams, useLocation } from "react-router-dom";
import GameHeader from '../components/GameHeader.tsx'
import PlayField from '../components/PlayField.tsx';
import Result from '../components/Result.tsx'
import { fetchData } from '../services/api.ts'
import { useNavigate } from 'react-router-dom'
import '../styles/PlayGame.css'

type Question = {
    id: number,
    question: number,
    timeLimitEachQuestion: number
}

export default function PlayGame() {
    const { id } = useParams();
    const location = useLocation();
    const { durationInput, name } = location.state || {};
    const navigate = useNavigate();

    const [question, setQuestion] = useState<Question | null>(null);
    const [instruction, setInstruction] = useState<string>("");
    const [timer, setTimer] = useState<number>(durationInput);
    const [isShowTimesUp, setIsShowTimesUp] = useState<boolean>(false);
    const [isShowPlayField, setIsShowPlayField] = useState<boolean>(true);
    const [isShowResult, setIsShowResult] = useState<boolean>(false);


    //create new attempt once when mounted
    useEffect(() => {
        const getFirstQuestion = async () => {
            const options = {
                method: "POST",
                body:  {
                    duration: durationInput,
                    userId: 1,
                    gameId: id
                }
            };

            const data = await fetchData("attempts", options)

            if (data) {
                setQuestion(data);
            }
        }

        getFirstQuestion();
    }, []);

    //start timer when mounted
    useEffect(() => {
        const countdownInterval = setInterval(() => {
            setTimer((prevTime) => {
                if (prevTime < 1) {
                    clearInterval(countdownInterval);
                    handleTimesUp();
                    return 0;
                }
                return prevTime - 1;
            });
        }, 1000);

        return () => {
            clearInterval(countdownInterval);
        }
    }, []);

    const handleTimesUp = () => {
        setIsShowTimesUp(true);
        setIsShowPlayField(false);

        //after 2s hide message time's out and show result
        const timeout = setTimeout(() => {
            setIsShowTimesUp(false);
            setIsShowResult(true);
            setInstruction("Final result:");
        }, 2000);

        return () => clearTimeout(timeout);
    };

    const navigateBackHome = () => {
        navigate("/");
    }

    return (
        <div>
            <GameHeader
                mainHeader={name}
                instruction={instruction}
            />
            {isShowPlayField && (
                <h4 className="total-time-left">Total time left: {timer}s</h4>
            )}
            {isShowPlayField && (
                < PlayField
                    id={question?.id ?? 0}
                    question={question?.question ?? 0}
                    timeLimit={question?.timeLimitEachQuestion ?? 0}
                />
            )}
            {isShowTimesUp && (
                <div>
                    <span className="times-up">Time's up!</span>
                </div>
            )}
            {isShowResult && (
                <div>
                    <Result id={question?.id ?? 0} />
                    <button onClick={navigateBackHome}>
                        Return to Home page
                    </button>
                </div>
            )}
        </div>
    )
}