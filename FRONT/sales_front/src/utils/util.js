export const toLetter=(nombre)=> {
  const unites = ["", "Un", "Deux", "Trois", "Quatre", "Cinq", "Six", "Sept", "Huit", "Neuf"];
  const dixUnites = ["", "Onze", "Douze", "Treize", "Quatorze", "Quinze", "Seize", "Dix-Sept", "Dix-Huit", "Dix-Neuf"];
  const dizaines = ["", "Dix", "Vingt", "Trente", "Quarante", "Cinquante", "Soixante", "Soixante-", "Quatre-Vingt"];
  const centaines = ["", "Cent", "Deux-Cents", "Trois-Cents", "Quatre-Cents", "Cinq-Cents", "Six-Cents", "Sept-Cents", "Huit-Cents", "Neuf-Cents"];

  function convertirBlocTroisChiffres(bloc) {
    const centaine = Math.floor(bloc / 100);
    const reste = bloc % 100;

    let resultat = "";

    if (centaine > 0) {
      resultat += centaines[centaine] + " ";
    }

    if (reste > 0) {
      if (reste < 10) {
        resultat += unites[reste];
      } else if (reste < 20) {
        resultat += dixUnites[reste - 10];
      } else {
        const dizaine = Math.floor(reste / 10);
        const unite = reste % 10;
        resultat += dizaines[dizaine];
        if (unite > 0) {
          resultat += "-" + unites[unite];
        }
      }
    }

    return resultat;
  }

  if (nombre === 0) {
    return "Zéro";
  }

  const billions = Math.floor(nombre / 1000000000000);
  const millions = Math.floor((nombre % 1000000000000) / 1000000);
  const milliers = Math.floor((nombre % 1000000) / 1000);
  const unite = nombre % 1000;

  let resultat = "";

  if (billions > 0) {
    resultat += convertirBlocTroisChiffres(billions) + " Milliard ";
  }

  if (millions > 0) {
    resultat += convertirBlocTroisChiffres(millions) + " Million ";
  }

  if (milliers > 0) {
    resultat += convertirBlocTroisChiffres(milliers) + " Mille ";
  }

  if (unite > 0) {
    resultat += convertirBlocTroisChiffres(unite);
  }

  return resultat.trim();
}
