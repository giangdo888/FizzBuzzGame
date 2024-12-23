interface CardProps {
    name: string;
    onClick: () => void;
}

export default function GameCard({ name, onClick }: CardProps) {

    return (
        <button className="game-card-button" onClick={onClick}>
            {name}
        </button>
    );
}