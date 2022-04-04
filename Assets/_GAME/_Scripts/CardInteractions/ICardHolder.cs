using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardHolder
{
    public IList<CardActor> Cards { get; }

    public bool AddCard(CardActor card);
    public bool RemoveCard(CardActor card);
    public CardActor GetCardAtPosition(int position);
    public void UpdateCardPositions();
}
