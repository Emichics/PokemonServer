import { getGenera } from "./api.js";

export async function loadGenus() {
    const pokemonGenus =
        document.getElementById("pokemonGenus");

    try {
        const result = await getGenera();

        result.data.forEach(genus => {
            const option =
                document.createElement("option");

            option.value = genus.name;
            option.textContent = genus.name;

            pokemonGenus.appendChild(option);
        });

    } catch (error) {
        console.error(error);
    }
}