   M S B A M L   `   `   `  ����^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  5   	   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	   F BDataManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DataManagerDataManager 5   	   #   DataManager.DownloadLogWindow   5   	   WwfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  5   	   Gwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DataManager     5   	     � Title$  下载日志��5
   	   $	��600��6   $
��1000��6#    � 
ResizeMode$ CanMinimize=�60   .��5      � .+�5   
   7   1System.Windows.Forms.Integration.WindowsFormsHost  $��0��6   $��0,60,0,0q�6+      Child. 5      '  !System.Windows.Forms.DataGridView  -   5        DG4��"  System.Windows.Forms.Control   Dock$
 Fill=�5   +      SelectionMode$ FullRowSelect=�67      RowHeadersVisible .  6U   5      �� 5   
   ���	#FFF4F4F5$	��Top=�6%   $��1��6=   $��0,50,0,0q�6H   
���   �6Z   �� 5   
   $��职员编号:��6   $
��Left=�6    $��5,10,0,0q�6;   $	��Top=�6M   �� 5   
   -   5         ��Name$ txt_StaffNumber��5      $
��Left=�6+   $��25��6F   $��	63,12,0,0q�6R    �� TextWrapping$
 Wrap=�6e   $	��Top=�6z   $	��120��6�   �� 5   
   $��姓名:��6   $
��Left=�6   $��
204,10,0,0q�69   $	��Top=�6M   �� 5   
   -   5        $ 
txt_People��5      $
��Left=�6&   $��25��6A   $��
238,12,0,0q�6M   $
 Wrap=�6a   $	��Top=�6v   $	��120��6�   �� 5   
   $��下载时间:��6   $
��Left=�6    $��
380,10,0,0q�6;   $	��Top=�6O   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dp_OrginTime��5      $
��Left=�6)   $��
438,12,0,0q�6D   $	��Top=�6X   $��12(�6p   �� 5   
   $��-��6   $
��Left=�6   $��
518,11,0,0q�67   $	��Top=�6K     5   
   -   5        $��
dp_EndTime��5      $
��Left=�6'   $��
531,12,0,0q�6B   $	��Top=�6V   �� 5   
   -   5        $��查询��5      $
��Left=�67   $��
762,10,0,0q�6R   $	��Top=�6f   $��25��6~   $��75��6�   5      5      