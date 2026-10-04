import { getPokemons, getPokemonDetail } from "./api.js";

let currentPage = 1;
const pageSize = 20;

let currentPokemons = [];

export async function loadPokemons() {
    const pokemonGrid = document.getElementById("pokemonGrid");
    const pokemonName = document.getElementById("pokemonName");
    const pokemonGenus = document.getElementById("pokemonGenus");

    try {
        const name = pokemonName.value.trim();
        const genus = pokemonGenus.value;

        const result = await getPokemons(
            currentPage,
            pageSize,
            name,
            genus
        );

        currentPokemons = result.data.items;

        renderPokemons(currentPokemons);
        updatePagination(result.data);

    } catch (error) {
        console.error(error);

        pokemonGrid.innerHTML = `
            <tr>
                <td colspan="4" class="text-center text-danger">
                    Error al cargar los Pokémon.
                </td>
            </tr>
        `;
    }
}

function renderPokemons(pokemons) {
    const pokemonGrid = document.getElementById("pokemonGrid");

    pokemonGrid.innerHTML = pokemons.map(pokemon => `
        <tr>
            <td>${pokemon.id}</td>

            <td>
                <img
                    src="${pokemon.image}"
                    alt="${pokemon.name}"
                    width="80"
                    height="80">
            </td>

            <td class="text-capitalize">
                ${pokemon.name}
            </td>

            <td>
                <div class="d-flex justify-content-center align-items-center gap-2 h-100">
                    <button
                        class="btn btn-primary"
                        data-pokemon-id="${pokemon.id}">
                        Ver detalle
                    </button>

                    <button
                        class="btn btn-info"
                        data-email-pokemon-id="${pokemon.id}">
                        Enviar correo
                    </button>
                </div>
            </td>
        </tr>
    `).join("");
}

function updatePagination(result) {
    const paginationInfo = document.getElementById("paginationInfo");
    const btnPrevious = document.getElementById("btnPrevious");
    const btnNext = document.getElementById("btnNext");

    paginationInfo.textContent = `Página ${result.page} de ${result.totalPages}`;

    btnPrevious.disabled = result.page <= 1;
    btnNext.disabled = result.page >= result.totalPages;
}

export function getCurrentPokemons() {
    return currentPokemons;
}

export function goToFirstPage() {
    currentPage = 1;
}

export function previousPage() {
    if (currentPage > 1) {
        currentPage--;
        loadPokemons();
    }
}

export function nextPage() {
    currentPage++;
    loadPokemons();
}

export async function loadPokemonDetail(id) {
    try {
        const result = await getPokemonDetail(id);
        const pokemon = result.data;

        document.getElementById("pokemonDetailImage").src = pokemon.image;
        document.getElementById("pokemonDetailImage").alt = pokemon.name;
        document.getElementById("pokemonDetailId").textContent = pokemon.id;
        document.getElementById("pokemonDetailName").textContent = pokemon.name;
        document.getElementById("pokemonDetailHeight").textContent = pokemon.height;
        document.getElementById("pokemonDetailWeight").textContent = pokemon.weight;
        document.getElementById("pokemonDetailTypes").textContent = pokemon.types.join(", ");

        const pokemonModal =
            bootstrap.Modal.getOrCreateInstance(
                document.getElementById("pokemonModal")
        );

        pokemonModal.show();

    } catch (error) {
        console.error(error);
    }
}