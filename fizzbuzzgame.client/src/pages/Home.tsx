import { useState, useEffect } from 'react';
import GameCard from '../components/GameCard.tsx'
import GameHeader from '../components/GameHeader.tsx'
import GameDetailPanel, { Rule } from '../components/GameDetailPanel.tsx'
import GameModal from '../components/GameModal.tsx'
import { fetchData } from '../services//api.ts'
import '../styles/Home.css'

export default function Home() {
    const [cards, setCards] = useState<any[]>([]);
    const [isGameDetailVisible, setGameDetailVisible] = useState<boolean>(false);
    const [selectedCardId, setSelectedCardId] = useState<number | null>(null);
    const [isOpenGameModal, setIsOpenGameModal] = useState<boolean>(false);

    const toggleDetailPanel = (id: number) => {
        //if click the same card -> toggle visibility | if not the same card -> show game datail
        if (selectedCardId == id) {
            setGameDetailVisible(!isGameDetailVisible);
        } else {
            setSelectedCardId(id);
            setGameDetailVisible(true);
        }
    }


    const closeDetailPanel = () => {
        setGameDetailVisible(false);
        setSelectedCardId(null);
    }

    const openGameModal = () => {
        setIsOpenGameModal(true);
    }

    const closeGameModal = () => {
        setIsOpenGameModal(false);
    }

    const getCardData = async () => {
        const data = await fetchData("games");
        if (data) {
            setCards(data);
        }
    }
    useEffect(() => {
        getCardData();
    }, []);

    return (
        <div>
            <GameHeader
                mainHeader="Welcome to FizzBuzz!"
                instruction="Please select a game to start"
            />
            <div className="card-list">
                {cards?.map((card) => (
                    <div key={card.id}>
                        <GameCard
                            name={card.name}
                            onClick={() => toggleDetailPanel(card.id)}
                        />
                        { isGameDetailVisible && selectedCardId == card.id && (
                            <GameDetailPanel
                                key={card.id}
                                id={card.id}
                                name={card.name}
                                minRange={card.minRange}
                                maxRange={card.maxRange}
                                rules={card.rules?.map((rule: Rule) => ({
                                    divisor: rule.divisor,
                                    word: rule.word
                                }))}
                                closeModal={closeDetailPanel}
                            />
                        )}
                    </div>
                ))}
                <div className="create-game">
                    <label htmlFor="create-game-button">Or</label><br />
                    <button className="create-game-button"
                        id="create-game-button"
                        onClick={openGameModal}
                    >
                        Create game
                    </button>
                </div>
                {isOpenGameModal && (
                    <GameModal closeModal={closeGameModal} onGameAdded={getCardData} />
                )}
            </div>
        </div>
    )
}
