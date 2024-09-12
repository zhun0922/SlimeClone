using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class DotweenUtil : SingletonPattern.Singleton<DotweenUtil>
{
    IEnumerator WaitSeconds(float time)
    {
        Debug.Log("대기 시작");

        yield return new WaitForSeconds(time);

        Debug.Log(time + "초가 경과했습니다.");
    }
    #region Move With Delay
    public Sequence MoveWithDelay(Transform originTrans, Vector3 targetVec, float duration, float startDelay = 0f)
    {
        float originalY = originTrans.position.y;

        Sequence delay = DOTween.Sequence();

        delay.Append(originTrans.DOMoveY(originalY, startDelay))  // 첫 번째 시퀀스: 딜레이 후 초기 위치로 이동 (아무 의미없음 단순히 딜레이를위한)
                .AppendCallback(() =>
                {
                    // 두 번째 시퀀스: 반복되는 효과 설정
                    Sequence mainSequence = DOTween.Sequence();
                    mainSequence.Append(originTrans.DOMove(targetVec, duration).SetEase(Ease.InExpo));
                });

        return delay;
    }

    public Sequence ScaleWithDelay(Transform originTrans, float endSize, float duration, float startDelay)
    {
        float originalY = originTrans.position.y;

        Sequence delay = DOTween.Sequence();

        delay.Append(originTrans.DOMoveY(originalY, startDelay))  // 첫 번째 시퀀스: 딜레이 후 초기 위치로 이동 (아무 의미없음 단순히 딜레이를위한)
                .AppendCallback(() =>
                {
                    // 두 번째 시퀀스: 반복되는 효과 설정
                    Sequence mainSequence = DOTween.Sequence();
                    mainSequence.Append(originTrans.DOScale(endSize, duration));
                });

        return delay;
    }

    /// <summary>
    /// This Method Include Init image's Color to FadeOut or In. 
    /// if you Want Fade in => FadeWithDelay(img, 0, 1f ...) 
    ///  if you Want Fade Out => FadeWithDelay(img, 1f, 0 ...) 
    /// </summary>
    /// <param name="image"></param>
    /// <param name="startValue"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    /// <param name="startDelay"></param>
    /// <returns></returns>
    public Sequence FadeWithDelay(Image image, float startValue, float endValue, float duration, float startDelay)
    {
        image.color = new Color(1f, 1f, 1f, startValue);

        Sequence delay = DOTween.Sequence();

        delay.Append(image.DOFade(startValue, startDelay))  //아무의미 없는 코드 Just for Delay
                .AppendCallback(() =>
                {
                    // 두 번째 시퀀스: 반복되는 효과 설정
                    Sequence mainSequence = DOTween.Sequence();
                    mainSequence.Append(image.DOFade(endValue, duration));
                });

        return delay;
    }



    #endregion

    #region UI Popup 

    [SerializeField] float basicPopUpDurationTime = 0.3f;
    [SerializeField] float viewerPopUpDurationTime = 0.2f;



    // ! target has to be Init SetActive(false) State !
    public void OpenPopUp(GameObject target)
    {
        target.transform.localScale = Vector2.zero;

        target.transform.DOScale(Vector2.one, basicPopUpDurationTime)
            .SetEase(Ease.Linear)
            .OnStart(() => target.SetActive(true));
    }

    public void OpenPopUp4Viewer(GameObject target)
    {
        target.transform.localScale = Vector2.zero;

        target.transform.DOScale(Vector2.one, viewerPopUpDurationTime)
            .SetEase(Ease.Linear)
            .OnStart(() => target.SetActive(true));
    }

    //���� Window�� �ְ� �ִϸ��̼��� ������ ������Ʈ�� ������ ���� �� �Ʒ� ���� ��� (SetActive�帧 ��� �ѹ��� �ؾ� ��������� �ȳ�)

    /// <summary>
    /// panel = ��ü Window , target = ��� Obj
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="target"></param>
    public void OpenPopUp(GameObject panel, GameObject target)
    {
        target.transform.localScale = Vector2.zero;

        target.transform.DOScale(Vector2.one, basicPopUpDurationTime)
            .SetEase(Ease.Linear)
            .OnStart(() =>
            {
                panel.SetActive(true);
                target.SetActive(true);

            });
    }


    // ! target has to be Init SetActive(false) State !
    public void ClosePopUp(GameObject target)
    {

        target.transform.DOScale(Vector2.zero, basicPopUpDurationTime).SetEase(Ease.InBack)
            .OnComplete(() => target.SetActive(false));
    }

    //���� Window�� �ְ� �ִϸ��̼��� ������ ������Ʈ�� ������ ���� �� �Ʒ� ���� ��� (SetActive�帧 ��� �ѹ��� �ؾ� ��������� �ȳ�)
    public void ClosePopUp(GameObject panel, GameObject target)
    {
        target.transform.DOScale(Vector2.zero, basicPopUpDurationTime).SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                target.SetActive(false);
                panel.SetActive(false);
            });
    }

    #endregion

    //���� Ƣ�� ȿ��
    #region Unit Scale

    [SerializeField] float bounceRepeatInterval = 1.3f;

    //���� 
    public void Bounce(Transform trans)
    {
        int repeatCount = -1;  // -1�� ���� �ݺ�

        Sequence bounceSequence = DOTween.Sequence();
        bounceSequence.Append(trans.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f)
            .SetEase(Ease.OutQuad));

        bounceSequence.Append(trans.DOScale(Vector3.one, 0.8f)
            .SetEase(Ease.OutBounce, amplitude: 2f, period: 2f));


        bounceSequence.SetLoops(repeatCount, LoopType.Restart);
        bounceSequence.AppendInterval(bounceRepeatInterval);
        bounceSequence.SetId("Bounce");

        bounceSequence.Play();
    }


    //�̰� ��� ��õ
    public void DramaticBounce(Transform trans)
    {
        int repeatCount = -1;  // -1�� ���� �ݺ�

        Sequence dramaticBounceSequence = DOTween.Sequence();

        dramaticBounceSequence.Append(trans.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.35f)
            .SetEase(Ease.OutQuad));

        dramaticBounceSequence.Append(trans.DOScale(Vector3.one, 0.15f)
            .SetEase(Ease.OutQuad));

        dramaticBounceSequence.Append(trans.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.15f)
            .SetEase(Ease.OutQuad));

        dramaticBounceSequence.Append(trans.DOScale(Vector3.one, 0.15f)
            .SetEase(Ease.OutQuad));

        dramaticBounceSequence.Append(trans.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.1f)
            .SetEase(Ease.OutQuad));

        dramaticBounceSequence.Append(trans.DOScale(Vector3.one, 0.1f)
            .SetEase(Ease.OutQuad));


        dramaticBounceSequence.SetLoops(repeatCount, LoopType.Restart);
        dramaticBounceSequence.AppendInterval(bounceRepeatInterval);

        //IdȰ���Ͽ� TutorialCombineUnit.cs�� Exit()���� Kill
        dramaticBounceSequence.SetId("DramaticBounce");

        dramaticBounceSequence.Play();
    }
    #endregion

    #region Box Open


   // public delegate void BoxEffectCallBack(int index, List<PokerDefense.Item> items);
    /*public void BoxEffect(Transform trans, BoxEffectCallBack onComplete, int index, List<PokerDefense.Item> items)
    {
        Sequence boxEffectSequence = DOTween.Sequence();

        boxEffectSequence.Append(trans.DOMoveY(-265f, 0.7f)
           .SetEase(Ease.OutBounce));


        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 3), 0.03f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -3), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 4), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -4), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 5), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -5), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 5), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -5), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 6), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -6), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 6), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -6), 0.06f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 7), 0.05f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -7), 0.05f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 8), 0.05f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -8), 0.05f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 9), 0.05f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, -9), 0.05f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.Append(trans.DORotate(new Vector3(0, 0, 0), 0.025f)
            .SetEase(Ease.OutQuad));

        boxEffectSequence.SetId("BoxEffect");

        boxEffectSequence.AppendCallback(() =>
        {
            if (onComplete != null)
            {
                onComplete(index, items);
            }
        });

        boxEffectSequence.Play();
    }*/

    public void BoxScale(Transform trans, Action onComplete)
    {
        Sequence boxEffectSequence_Scale = DOTween.Sequence();
        boxEffectSequence_Scale.Append(trans.DOPunchScale(new Vector3(0.08f, 0.08f, 1f), 0.7f));

        boxEffectSequence_Scale.SetId("BoxScale");

        boxEffectSequence_Scale.AppendCallback(() =>
        {
            if (onComplete != null)
            {
                onComplete();
            }
        });

        boxEffectSequence_Scale.Play();
    }
    #endregion



    #region Floating Effect

    //Delay Just Once => Error Example//

    //When done this way, because each tween is created within the method and executed immediately, all tweens start at the same time, and after 1 second, which is the delay time of the last started tween, all tweens loop simultaneously.


    /*  public Sequence FloatingEffect(Transform transform, float ydiff, float durationTime, float startDelay)
      {
          float originalY = transform.position.y;

          Sequence justDelay = DOTween.Sequence(); //처음 시작에만 딜레이를 주기 위해 시퀀스를 다르게 함
          justDelay.Append(transform.transform.DOMoveY(originalY, startDelay)); // originalY에서 originalY로 이동. !! Just for Delay

          Sequence Loop = DOTween.Sequence();
          Loop.Append(transform.transform.DOMoveY(originalY - ydiff, durationTime).SetEase(Ease.InOutQuad));
          Loop.SetLoops(-1, LoopType.Yoyo);

          justDelay.AppendCallback(() =>
          {
              Loop.Play();
          });


         // justDelay.Play();

          return justDelay;
      }
  */




    //******************** HOW TO DELAY JUST ONCE **************************************//

    //When there are several objects floating on one screen, it may not be awkward if the startDelay is slightly different for each. (It seems better to fix the durationTime)
    public void FloatingEffect(Transform trans, float ydiff, float durationTime, float startDelay)
    {
        float originalY = trans.position.y;

        Sequence sequence = DOTween.Sequence();

        // 첫 번째 시퀀스: 딜레이 후 초기 위치로 이동
        sequence.Append(trans.DOMoveY(originalY, startDelay))
                .AppendCallback(() =>
                {
                    // 두 번째 시퀀스: 반복되는 효과 설정
                    Sequence loopSequence = DOTween.Sequence();
                    loopSequence.Append(trans.DOMoveY(originalY - ydiff, durationTime).SetEase(Ease.InOutQuad))
                                .SetLoops(-1, LoopType.Yoyo);
                });

        sequence.Play(); // 트윈 실행
    }

    #endregion

    #region JumpEffect

    public void JumpEffect(Transform trans, float ydiff, float duration)
    {
        float interval = 1f;
        Sequence sequence = DOTween.Sequence();

        Vector3 origin = trans.position;
        sequence.Append(trans.DOMoveY(ydiff, duration)).SetEase(Ease.OutFlash);
        // sequence.Append(transform.DOMove(origin, duration));

        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.AppendInterval(interval);

        sequence.Play();
    }

    public void JumpEffectWithDelay(Transform transform, float ydiff, float duration, float startDelay)
    {
        float interval = 1f;

        float originalY = transform.position.y;

        Sequence sequence = DOTween.Sequence();



        // 첫 번째 시퀀스: 딜레이 후 초기 위치로 이동
        sequence.Append(transform.DOMoveY(originalY, startDelay))
                .AppendCallback(() =>
                {
                    Sequence main = DOTween.Sequence();

                    main.Append(transform.DOMoveY(ydiff, duration).SetEase(Ease.OutFlash));
                    sequence.Append(transform.DOMoveY(-ydiff, duration - 0.2f));

                    main.SetLoops(-1, LoopType.Yoyo);
                    main.AppendInterval(interval);

                });

        sequence.Play();
    }
    #endregion

    #region ShakeRotate
    public void ShakeRoatate(Transform trans)
    {
        float interval = 1.3f;
        Sequence sequence = DOTween.Sequence();

        Vector3 origin = trans.position;


        sequence.Append(trans.DORotate(new Vector3(0, 0, 6), 0.06f)
          .SetEase(Ease.OutQuad));

        sequence.Append(trans.DORotate(new Vector3(0, 0, 0), 0.06f)
            .SetEase(Ease.OutQuad));

        sequence.Append(trans.DORotate(new Vector3(0, 0, -6), 0.06f)
            .SetEase(Ease.OutQuad));

        sequence.Append(trans.DORotate(new Vector3(0, 0, 0), 0.06f)
            .SetEase(Ease.OutQuad));

        sequence.Append(trans.DORotate(new Vector3(0, 0, 4), 0.05f)
            .SetEase(Ease.OutQuad));

        sequence.Append(trans.DORotate(new Vector3(0, 0, 0), 0.05f)
            .SetEase(Ease.OutQuad));

        sequence.Append(trans.DORotate(new Vector3(0, 0, -3), 0.05f)
            .SetEase(Ease.OutQuad));
        sequence.Append(trans.DORotate(new Vector3(0, 0, 0), 0.05f)
          .SetEase(Ease.OutQuad));


        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.AppendInterval(interval);

        sequence.Play();
    }
    #endregion

    #region MonsterInfo
    public void SlideEffect(Transform trans, float xDiff, float duration, bool includeInit, Vector3 origin, GameObject target)
    {
        Sequence sequence = DOTween.Sequence();

        //초기화
        //기기 해상도의 x좌표만큼 우측으로 이동
        if (includeInit)
        {
            trans.position = origin;

            float targetX = Screen.width;
            Debug.Log(targetX);
            Vector3 currentPos = trans.position;

            float newX = currentPos.x + targetX;
            trans.position = new Vector3(newX, currentPos.y, currentPos.z);
        }



        sequence.Append(trans.DOMove(target.transform.position, duration).SetEase(Ease.OutBack));


        sequence.Play();
    }

    /*  public void SlideEffect(Transform trans, float xDiff, float duration, bool includeInit, Vector3 origin)
      {
          Sequence sequence = DOTween.Sequence();

          //초기화
          //기기 해상도의 x좌표만큼 우측으로 이동
          if (includeInit)
          {
              trans.position = origin;

              float targetX = Screen.width;
              Debug.Log(targetX);
              Vector3 currentPos = trans.position;

              float newX = currentPos.x + targetX;
              trans.position = new Vector3(newX, currentPos.y, currentPos.z);
          }



          sequence.Append(trans.DOMoveX(xDiff, duration).SetEase(Ease.OutBack));


          sequence.Play();
      }*/
    #endregion
}