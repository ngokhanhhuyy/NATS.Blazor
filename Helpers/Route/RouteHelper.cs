namespace NATS.Helpers;

public class RouteHelper : IRouteHelper
{
    public string GetPublicHomeRoutePath()
    {
        return "/";
    }

    public string GetPublicAboutUsIntroductionRoutePath()
    {
        return "/ve-chung-toi";
    }

    public string GetPublicSummaryItemRoutePath(int? id = null)
    {
        return id.HasValue ? $"/gioi-thieu/{id}" : "/gioi-thieu";
    }

    public string GetPublicServiceListRoutePath()
    {
        return "/dich-vu";
    }

    public string GetPublicServiceDetailRoutePath(int id)
    {
        return $"/dich-vu/{id}";
    }

    public string GetPublicCourseListRoutePath()
    {
        return "/khoa-hoc";
    }

    public string GetPublicCourseDetailRoutePath(int id)
    {
        return $"/khoa-hoc/{id}";
    }

    public string GetPublicContactRoutePath()
    {
        return "/lien-he";
    }

    public string GetPublicEnquiryRoutePath()
    {
        return "/cau-hoi";
    }

    public string GetSignInRoutePath()
    {
        return "/dang-nhap";
    }
}

public interface IRouteHelper
{
    public string GetPublicHomeRoutePath();
    public string GetPublicAboutUsIntroductionRoutePath();
    public string GetPublicSummaryItemRoutePath(int? id = null);
    public string GetPublicServiceListRoutePath();
    public string GetPublicServiceDetailRoutePath(int id);
    public string GetPublicCourseListRoutePath();
    public string GetPublicCourseDetailRoutePath(int id);
    public string GetPublicContactRoutePath();
    public string GetPublicEnquiryRoutePath();
    public string GetSignInRoutePath();
}