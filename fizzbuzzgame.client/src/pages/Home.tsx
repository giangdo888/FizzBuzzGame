import { useState, useEffect } from 'react';
import GameCard from '../components/GameCard.tsx'
import GameHeader from '../components/GameHeader.tsx'
import GameDetailPanel, { Rule } from '../components/GameDetailPanel.tsx'
import { fetchData } from '../services//api.ts'

export default function Home() {
    const [cards, setCards] = useState<any[]>([]);
    const [isGameDetailVisible, setGameDetailVisible] = useState<boolean>(false);
    const [selectedCardId, setSelectedCardId] = useState<number | null>(null);

    const toggleDetailPanel = (id: number) => {
        //if click the same card -> toggle visibility | if not the same card -> show game datail
        if (selectedCardId == id) {
            setGameDetailVisible(!isGameDetailVisible);
        } else {
            setSelectedCardId(id);
            setGameDetailVisible(true);
        }
    }

    useEffect(() => {
        const getCardData = async () => {
            const data = await fetchData("games");
            if (data) {
                setCards(data);
            }
        }

        getCardData();
    }, []);

    return (
        <div>
            <GameHeader
                mainHeader="Welcome to FizzBuzz!"
                instruction="Please select a game to start"
            />
            <div>
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
                                rules={card.rules?.map((rule : Rule) => ({
                                    divisor: rule.divisor,
                                    word: rule.word
                                }))}
                            />
                        )}
                    </div>
                ))}
            </div>
        </div>
    )
}
