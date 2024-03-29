using UnityEngine;

public class PolicyMenu : MonoBehaviour
{
    private string policyKey = "policy";
    void Start()
    {
        var accepted = PlayerPrefs.GetInt(policyKey, 0) == 1;
        if (accepted)
            return;
        
        new TermsOfServiceDialog().
                SetTermsOfServiceLink( "https://feed-the-fish-0.flycricket.io/terms.html" ).
                SetPrivacyPolicyLink( "https://feed-the-fish-0.flycricket.io/privacy.html" ).ShowDialog(OnMenuClosed );
    }

    private void OnMenuClosed()
    {
        Debug.LogWarning("Policy accepted");
        PlayerPrefs.SetInt(policyKey, 1);
    }

}
