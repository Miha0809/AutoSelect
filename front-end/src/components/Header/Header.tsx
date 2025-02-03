import styles from "./Header.module.scss";

export const Header: React.FC = () => {
    return (
        <header className={styles.header}>
            <h1>Header</h1>
        </header>
    );
}