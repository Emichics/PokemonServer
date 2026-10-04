import { sendPokemonEmail } from "./api.js";

let selectedPokemonId = null;

export function openPokemonEmailModal(pokemonId) {
    selectedPokemonId = pokemonId;

    document.getElementById("emailInput").value = "";
    document.getElementById("emailError").textContent = "";

    const emailModal =
        bootstrap.Modal.getOrCreateInstance(
            document.getElementById("emailModal")
        );

    emailModal.show();
}

export function openGeneralEmailModal() {
    selectedPokemonId = null;

    document.getElementById("emailInput").value = "";
    document.getElementById("emailError").textContent = "";

    const emailModal =
        bootstrap.Modal.getOrCreateInstance(
            document.getElementById("emailModal")
        );

    emailModal.show();
}

export async function confirmSendEmail(pokemons) {

    const emailInput = document.getElementById("emailInput");
    const emailError = document.getElementById("emailError");
    const email = emailInput.value.trim();

    if (!email) {
        emailError.textContent = "Ingresa un correo electrónico.";
        return;
    }

    try {
        await sendPokemonEmail(
            selectedPokemonId,
            email,
            pokemons
        );

        const emailModal =
            bootstrap.Modal.getOrCreateInstance(
                document.getElementById("emailModal")
            );

        emailModal.hide();

        alert("Correo enviado correctamente.");

    } catch (error) {
        console.error(error);

        emailError.textContent = "No fue posible enviar el correo.";
    }
}