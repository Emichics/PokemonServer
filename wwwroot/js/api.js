export async function getGenera() {
    const response = await fetch("/api/catalog/genus");

    if (!response.ok) {
        throw new Error("Error al obtener los genus.");
    }

    return await response.json();
}

export async function getPokemons(page, pageSize, name, genus) {
    const params = new URLSearchParams({
        page,
        pageSize
    });

    if (name) {
        params.set("name", name);
    }

    if (genus) {
        params.set("genus", genus);
    }

    const response = await fetch(`/api/pokemon?${params}`);

    if (!response.ok) {
        throw new Error("Error al obtener los Pokémon.");
    }

    return await response.json();
}

export async function getPokemonDetail(id) {
    const response = await fetch(`/api/pokemon/${id}`);

    if (!response.ok) {
        throw new Error("Error al obtener el detalle del Pokémon.");
    }

    return await response.json();
}

export async function exportPokemons(pokemons) {
    const response = await fetch("/api/pokemon/export", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            pokemons
        })
    });

    if (!response.ok) {
        throw new Error("Error al exportar los Pokémon.");
    }
    
    return await response.blob();
}

export async function sendPokemonEmail( pokemonId, recipient, pokemons) {
    let url;
    let body;

    if (pokemonId !== null) {
        url = `/api/pokemon/${pokemonId}/sendemail`;

        body = {
            recipient
        };
    } else {
        url = "/api/pokemon/sendemail";

        body = {
            recipient,
            pokemons
        };
    }

    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(body)
    });

    if (!response.ok) {
        throw new Error("Error al enviar el correo.");
    }
}