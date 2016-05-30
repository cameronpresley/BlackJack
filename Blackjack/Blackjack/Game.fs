﻿module Game

open Player
open Deck
open Dealer
open Infrastructure
open Participant
open Card

type Game = {Players:Player list; Dealer:Dealer; Deck:Deck}

let maybe = new MaybeBuilder()

let CreateGame drawCard deck numPlayers =
    let createPlayers state id =
        maybe {
            let! players,deck = state
            let! player,deck = CreatePlayer drawCard deck id
            return (players@[player], deck)
        }

    maybe {
        let! dealer,deck = CreateDealer drawCard deck
        let initialState = Some([], deck)
        let! players,deck = [1 .. numPlayers] |> List.fold createPlayers initialState
        return {Dealer=dealer; Players=players; Deck=deck}
    }