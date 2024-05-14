export default function getPageTitle(pageTitle) {
  if (pageTitle) {
    return pageTitle; //`${pageTitle} - ${title}`;
  }
  return '';
}