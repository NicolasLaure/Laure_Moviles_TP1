﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMover : ManejoPallets
{
    public ManejoPallets Desde, Hasta;
    private InputReader _inputReader;
    bool segundoCompleto = false;

    public InputReader InputReader
    {
        set
        {
            _inputReader = value;
            _inputReader.onMove += HandleInput;
        }
    }

    private void OnDestroy()
    {
        if (_inputReader != null)
            _inputReader.onMove -= HandleInput;
    }
    // private void Update() {
    //     switch (miInput) {
    //         case MoveType.WASD:
    //             if (!HasBags() && Desde.HasBags() && Input.GetKeyDown(KeyCode.A)) {
    //                 PrimerPaso();
    //             }
    //             if (HasBags() && Input.GetKeyDown(KeyCode.S)) {
    //                 SegundoPaso();
    //             }
    //             if (segundoCompleto && HasBags() && Input.GetKeyDown(KeyCode.D)) {
    //                 TercerPaso();
    //             }
    //             break;
    //         case MoveType.Arrows:
    //             if (!HasBags() && Desde.HasBags() && Input.GetKeyDown(KeyCode.LeftArrow)) {
    //                 PrimerPaso();
    //             }
    //             if (HasBags() && Input.GetKeyDown(KeyCode.DownArrow)) {
    //                 SegundoPaso();
    //             }
    //             if (segundoCompleto && HasBags() && Input.GetKeyDown(KeyCode.RightArrow)) {
    //                 TercerPaso();
    //             }
    //             break;
    //         default:
    //             break;
    //     }
    // }

    private void HandleInput(Vector2 dir)
    {
        if (!HasBags() && Desde.HasBags() && dir.x < 0)
            PrimerPaso();

        if (HasBags() && dir.y < 0)
            SegundoPaso();

        if (segundoCompleto && HasBags() && dir.x > 0)
            TercerPaso();
    }

    void PrimerPaso()
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }

    void SegundoPaso()
    {
        base.bags[0].transform.position = transform.position;
        segundoCompleto = true;
    }

    void TercerPaso()
    {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor)
    {
        if (HasBags())
        {
            if (receptor.Recibir(bags[0]))
            {
                bags.RemoveAt(0);
            }
        }
    }

    public override bool Recibir(Pallet pallet)
    {
        if (!HasBags())
        {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}