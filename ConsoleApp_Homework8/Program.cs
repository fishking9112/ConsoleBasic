using System;
using System.Collections.Generic;

public enum Suit { Hearts, Diamonds, Clubs, Spades }
public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

// 카드 한 장을 표현하는 클래스
public class Card
{
    //카드의 무늬
    public Suit Suit { get; private set; }

    //카드의 숫자
    public Rank Rank { get; private set; }

    public Card(Suit s, Rank r)
    {
        Suit = s;
        Rank = r;
    }

    //점수를 반환하는 함수
    public int GetValue()
    {
        // 10보다 작으면 숫자 그대로
        if ((int)Rank <= 10)
        {
            return (int)Rank;
        }
        // 10보다 크고 14보다 작으면 10으로 친다. ( J , Q , K )
        else if ((int)Rank <= 13)
        {
            return 10;
        }
        else
        {
            // 나머지 ( 에이스 는 11 )
            return 11;
        }
    }

    // 카드의 무늬와 숫자를 문자열로 반환해주는 함수
    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

// 덱을 표현하는 클래스
public class Deck
{
    // 덱에있는 카드 뭉치
    private List<Card> cards;

    public Deck()
    {
        cards = new List<Card>();

        //2중 반복문을 통해 , 모든 무늬와 숫자에 관해 카드 생성
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                //을 덱에 넣어준다.
                cards.Add(new Card(s, r));
            }
        }

        //셔플
        Shuffle();
    }

    //카드를 섞는 함수
    public void Shuffle()
    {
        Random rand = new Random();

        for (int i = 0; i < cards.Count; i++)
        {
            //버블
            int j = rand.Next(i, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    //셔플된 덱에서 한장의 카드를 뽑는 함수
    public Card DrawCard()
    {
        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}

// 패를 표현하는 클래스
public class Hand
{
    private List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    //카드 한장을 더 받는 함수
    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    //핸드 총점을 계산하는 함수
    public int GetTotalValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (Card card in cards)
        {
            //에이스의 갯수를 세준다
            if (card.Rank == Rank.Ace)
            {
                aceCount++;
            }
            total += card.GetValue();
        }

        //핸드에 에이스가 있고 , 총점이 21을 넘으면 에이스는 1점으로도 사용가능
        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }
}

// 플레이어를 표현하는 클래스
public class Player
{
    public Hand Hand { get; private set; }

    public Player()
    {
        Hand = new Hand();
    }

    // 플레이어가 카드를 뽑는 함수
    public Card DrawCardFromDeck(Deck deck)
    {
        Card drawnCard = deck.DrawCard();
        Hand.AddCard(drawnCard);
        return drawnCard;
    }
}

// 여기부터는 학습자가 작성
// 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
public class Dealer : Player
{
    // 코드를 여기에 작성하세요

    // 딜러는 17밑에선 카드를 무조건 뽑아야 함.
    public void DealerDraw(Deck _deck)
    {
        while(Hand.GetTotalValue() < 17)
        {
            Card drawCard = DrawCardFromDeck(_deck);
            Console.WriteLine("딜러가 뽑은 카드 : {0}", drawCard.ToString());
        }
    }
}

// 블랙잭 게임을 구현하세요. 
public class Blackjack
{
    // 코드를 여기에 작성하세요

    //선언
    private Player player;
    private Dealer dealer;
    private Deck deck;

    public void PlayBlackjack()
    {
        player = new Player();
        dealer = new Dealer();
        deck   = new Deck();

        //시작시 플레이어 , 딜러는 2장의 카드를 받습니다.
        for (int i = 0; i < 2; i++)
        {
            player.DrawCardFromDeck(deck);
            dealer.DrawCardFromDeck(deck);
        }
        //게임 시작
        Console.WriteLine("== Game Start ! ==");
        Console.WriteLine(" :: Player Card :: ");
        Console.WriteLine("플레이어 카드 합 : {0}", player.Hand.GetTotalValue());
        Console.WriteLine("딜러 카드 합 : {0}", dealer.Hand.GetTotalValue());

        //플레이어는 21점이 넘지 않는한 계속해서 카드를 받을 수 있습니다.
        Console.WriteLine(" == 플레이어 턴 == ");
        while (player.Hand.GetTotalValue() < 21)
        {
            Console.Write("히트 ? (y / n) : ");
            string input = Console.ReadLine();

            if(input == "y")
            {
                Card newCard = player.DrawCardFromDeck(deck);
                Console.WriteLine("새로 뽑은 카드 : {0}", newCard.ToString());
                Console.WriteLine("플레이어 카드 합 : {0}", player.Hand.GetTotalValue());
            }
            else
            {
                break;
            }
        }

        //딜러는 카드합이 17이 되거나 , 넘을때까지 계속해서 받아야 한다.
        Console.WriteLine(" == 딜러 턴 == ");
        Console.WriteLine(" :: Dealer Card :: ");
        dealer.DealerDraw(deck);
        Console.WriteLine("딜러 카드 합 : {0}", dealer.Hand.GetTotalValue());

        //승패

        //카드를 더이상 받지 않는 플레이어와 딜러 중 카드 합이 21점에 가까운 쪽이 승리
        //21을 초과하면 패배
        if (player.Hand.GetTotalValue() > 21)
        {
            Console.WriteLine("플레이어 카드의 합이 21점을 초과한 {0}점 입니다. Dealer 의 승리 !! " , player.Hand.GetTotalValue());
        }
        else if (dealer.Hand.GetTotalValue() > 21)
        {
            Console.WriteLine("딜러 카드의 합이 21점을 초과한 {0}점 입니다. Player 의 승리 !! ", dealer.Hand.GetTotalValue());
        }
        else if(player.Hand.GetTotalValue() > dealer.Hand.GetTotalValue())
        {
            Console.WriteLine("플레이어 카드의 합이 딜러보다 더 높습니다 ! . Player 의 승리 !! ");
        }
        else
        {
            Console.WriteLine("딜러 카드의 합이 플레이어보다 더 높습니다 ! . Dealer 의 승리 !! ");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 블랙잭 게임을 실행하세요
        Blackjack blackjack = new Blackjack();
        blackjack.PlayBlackjack();
    }
}