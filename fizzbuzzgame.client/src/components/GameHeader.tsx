type GameCardProps = {
	mainHeader: string;
	instruction: string;
};

export default function GameHeader({ mainHeader, instruction }: GameCardProps) {
	return (
		<header className="main-header">
			<h1>{mainHeader}</h1>
			<h3>{instruction}</h3>
		</header>
	);
}