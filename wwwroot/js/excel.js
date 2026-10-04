import { exportPokemons } from "./api.js";

export async function exportCurrentPokemons(pokemons) {
    try {
        const blob = await exportPokemons(pokemons);
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement("a");

        link.href = url;
        link.download = "pokemons.xlsx";

        link.click();

        window.URL.revokeObjectURL(url);

    } catch (error) {
        console.error(error);
    }
}