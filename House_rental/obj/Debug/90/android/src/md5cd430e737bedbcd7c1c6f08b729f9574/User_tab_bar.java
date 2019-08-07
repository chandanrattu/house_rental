package md5cd430e737bedbcd7c1c6f08b729f9574;


public class User_tab_bar
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("House_rental.User_tab_bar, House_rental", User_tab_bar.class, __md_methods);
	}


	public User_tab_bar ()
	{
		super ();
		if (getClass () == User_tab_bar.class)
			mono.android.TypeManager.Activate ("House_rental.User_tab_bar, House_rental", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
