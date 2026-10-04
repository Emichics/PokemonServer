import {
    loadPokemons,
    loadPokemonDetail,
    getCurrentPokemons,
    goToFirstPage,
    previousPage,
    nextPage
} from "./pokemon.js";

import { loadGenus } from "./catalog.js";

import {
    openPokemonEmailModal,
    openGeneralEmailModal,
    confirmSendEmail
} from "./email.js";

import { exportCurrentPokemons } from "./excel.js";

import {
    showLoading,
    hideLoading
} from "./ui.js";

document.addEventListener("DOMContentLoaded", async () => {

    const pokemonGrid = document.getElementById("pokemonGrid");
    const btnSearch = document.getElementById("btnSearch");
    const btnPrevious = document.getElementById("btnPrevious");
    const btnNext = document.getElementById("btnNext");
    const btnExport = document.getElementById("btnExport");
    const btnSendEmail = document.getElementById("btnSendEmail");
    const btnConfirmSendEmail = document.getElementById("btnConfirmSendEmail");

    pokemonGrid.addEventListener("click", event => {

        const detailButton = event.target.closest("[data-pokemon-id]");
        const emailButton = event.target.closest("[data-email-pokemon-id]");

        if (detailButton) {
            const pokemonId = detailButton.dataset.pokemonId;
            loadPokemonDetail(pokemonId);
            return;
        }

        if (emailButton) {
            const pokemonId = emailButton.dataset.emailPokemonId;
            openPokemonEmailModal(pokemonId);
        }
    });


    btnSearch.addEventListener("click", async () => {

        goToFirstPage();
        showLoading();
        try {
            await loadPokemons();
        } finally {
            hideLoading();
        }
    });


    btnPrevious.addEventListener("click", async () => {

        showLoading();

        try {
            await previousPage();
        } finally {
            hideLoading();
        }
    });


    btnNext.addEventListener("click", async () => {

        showLoading();

        try {
            await nextPage();
        } finally {
            hideLoading();
        }
    });


    btnExport.addEventListener("click", async () => {

        showLoading();

        try {
            await exportCurrentPokemons(
                getCurrentPokemons()
            );
        } finally {
            hideLoading();
        }
    });


    btnSendEmail.addEventListener("click", () => {
        openGeneralEmailModal();
    });


    btnConfirmSendEmail.addEventListener("click", async () => {
            showLoading();

            try {
                await confirmSendEmail(
                    getCurrentPokemons()
                );
            } finally {
                hideLoading();
            }
        }
    );


    showLoading();

    try {
        await loadGenus();
        await loadPokemons();
    } finally {
        hideLoading();
    }
});