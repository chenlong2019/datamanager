   M S B A M L   `   `   `  ����^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  5   
   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	   F BDataManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DataManagerDataManager 5   	   !   DataManager.AddUserNameForm   5   	   WwfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  5   
   Gwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DataManager     5   	     � Title$  管理用户��5
   	   $	��600��6   $
��1000��6#    � 
ResizeMode$ CanMinimize=�60    � WindowStartupLocation$ CenterScreen=�6I   .��5      � .+�5   
   �� $��	0,100,0,0q�6   .��5      7   1System.Windows.Forms.Integration.WindowsFormsHost  $��0��6   $��0,0,0,0q�6/      Child. 5      '  !System.Windows.Forms.DataGridView  -   5        DG1��"  System.Windows.Forms.Control   Dock$
 Fill=�5   /      SelectionMode$ FullRowSelect=�6;      ColumnHeadersVisible .    RowHeadersVisible .  6�   5      5      �� 5   
   $��用户名：��6   $
��Left=�6   $	��Top=�6:   $��	10,10,0,0q�6R   �� 5   
   $��职员编号：��6   $
��Left=�6    $��
185,10,0,0q�6;   $	��Top=�6O   �� 5   
   $��职员姓名：��6   $
��Left=�6    $��
370,10,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $��txt_UserName��5      $
��Left=�6&   $��	60,15,0,0q�6B    �� TextWrapping$
 Wrap=�6U   $	��Top=�6j   $	��120��6�   �� 5   
   -   5        $��txt_StaffNumber��5      $
��Left=�6)   $��
245,15,0,0q�6E   $
 Wrap=�6Y   $	��Top=�6n   $	��120��6�   �� 5   
   -   5        $��txt_Name��5      $
��Left=�6"   $��
430,15,0,0q�6>   $
 Wrap=�6R   $	��Top=�6g   $	��120��6   �� 5   
   -   5        $��btn_SelectUser��5      $��查询��6   $
��Left=�6Q   $��
576,10,0,0q�6l   $	��Top=�6�   $��75��6�   $��25��6�   �� 5   
   ���	#FFF4F4F5$��1��6&   $��0,50,0,0q�61   
���   �$	��Top=�6R   �� 5   
   -   5        $��注册��5      $
��Left=�63   $��	10,65,0,0q�6N   $	��Top=�6a   $��75��6y   $��25��6�   �� 5   
   -   5        $��修改��5      $��25��68   $
��Left=�6D   $	��Top=�6_   $��	95,65,0,0q�6w   $��75��6�   �� 5   
   -   5        $��删除��5      $��25��6<   $
��Left=�6H   $	��Top=�6c   $��
180,65,0,0q�6{   $��75��6�   5      5      