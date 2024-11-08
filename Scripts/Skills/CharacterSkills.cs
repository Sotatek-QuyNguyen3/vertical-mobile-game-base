
using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterSkills : MonoBehaviour
{
    // lightning
    private float lightningCooldown = 30f;
    private float lightningDuration = 3f;
    [SerializeField] private GameObject lightningSkillMask;
    [SerializeField] private GameObject needleLightningTransform;
    [SerializeField] private TextMeshProUGUI cooldownLightningSkillText;
    // durable
    private float durableCooldown = 25f;
    private float durableDuration = 5f;
    [SerializeField] private GameObject durableSkillMask;
    [SerializeField] private GameObject needleDurableTransform;
    [SerializeField] private TextMeshProUGUI cooldownDurableSkillText;
    // focus
    private float focusCooldown = 20f;
    private float focusDuration = 4f;
    [SerializeField] private GameObject focusSkillMask;
    [SerializeField] private GameObject needleFocusTransform;
    [SerializeField] private TextMeshProUGUI cooldownFocusSkillText;
    // stun
    private float stunCooldown = 40f;
    private float stunDuration = 1.5f;
    [SerializeField] private GameObject stunSkillMask;
    [SerializeField] private GameObject needleStunTransform;
    [SerializeField] private TextMeshProUGUI cooldownStunSkillText;
    private IEnumerator CooldownTimer(GameObject mask, float cooldown, TextMeshProUGUI cooldownText)
    {
        mask.SetActive(true);

        for (int i = (int)cooldown; i > 0; i--)
        {
            cooldownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cooldownText.text = "";

        mask.SetActive(false);
    }
    private IEnumerator RotateNeedle(GameObject needleTransform, float duration)
    {
        float elapsedTime = 0f;
        float fullRotation = 360f; // Kim quay 1 vòng là 360 độ
        float rotationSpeed = fullRotation / duration; // Tốc độ quay để hoàn thành 1 vòng trong lightningDuration

        // Đảm bảo kim được kích hoạt khi bắt đầu quay
        needleTransform.gameObject.SetActive(true);

        while (elapsedTime < duration)
        {
            // Tính toán góc quay mỗi khung hình
            float angle = rotationSpeed * Time.deltaTime;
            needleTransform.transform.Rotate(0f, 0f, -angle); // Xoay theo chiều kim đồng hồ

            elapsedTime += Time.deltaTime;
            yield return null; // Chờ cho khung hình tiếp theo
        }

        // Đảm bảo kim về vị trí ban đầu sau khi hoàn thành
        needleTransform.transform.rotation = Quaternion.identity;
        needleTransform.gameObject.SetActive(false);
    }
    // lightning skill
    public IEnumerator ActivateLightningSkill()
    {
        needleLightningTransform.SetActive(true);
        StartCoroutine(RotateNeedle(needleLightningTransform, lightningDuration));

        yield return new WaitForSeconds(lightningDuration);

        StartCoroutine(CooldownTimer(lightningSkillMask, lightningCooldown, cooldownLightningSkillText));
    }
    // durable skill
    public IEnumerator ActivateDurableSkill()
    {
        needleDurableTransform.SetActive(true);
        StartCoroutine(RotateNeedle(needleDurableTransform, durableDuration));

        yield return new WaitForSeconds(durableDuration);

        StartCoroutine(CooldownTimer(durableSkillMask, durableCooldown, cooldownDurableSkillText));
    }
    // focus skill
    public IEnumerator ActivateFocusSkill()
    {
        needleFocusTransform.SetActive(true);
        StartCoroutine(RotateNeedle(needleFocusTransform, focusDuration));

        yield return new WaitForSeconds(focusDuration);

        StartCoroutine(CooldownTimer(focusSkillMask, focusCooldown, cooldownFocusSkillText));
    }
    // stun skill
    public IEnumerator ActivateStunSkill()
    {
        needleStunTransform.SetActive(true);
        StartCoroutine(RotateNeedle(needleStunTransform, stunDuration));

        yield return new WaitForSeconds(stunDuration);

        StartCoroutine(CooldownTimer(stunSkillMask, stunCooldown, cooldownStunSkillText));
    }
}
